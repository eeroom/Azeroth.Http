using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpFile {
    /// <summary>
    /// Download03 的摘要说明
    /// </summary>
    public class Download4Range : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            
            long fileLength = 50*1024*1024;
            string fileName = "测试下载50MB.zip";

            context.Response.ContentType = "application/octet-stream";
            //这涉及RFC标准
            context.Response.AddHeader("Content-Disposition", "attachment;filename*=utf-8'zh_cn'" + context.Server.UrlEncode(fileName));
            long start = 0;
            long end = fileLength - 1;
            var range= context.Request.Headers["Range"]??string.Empty;
            var rangeFlag = "bytes=";
            if (range.StartsWith(rangeFlag)) {
                var lstRange = range.Substring(rangeFlag.Length).Split(new char[] { '-', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (lstRange.Count == 1) {
                    start = long.Parse(lstRange.First());
                } else if (lstRange.Count == 2) {
                    start = long.Parse(lstRange.First());
                    end = long.Parse(lstRange.Last());
                } else if (lstRange.Count > 2) {
                    throw new ArgumentException("服务端不支持多个range范围的下载请求");
                }
                //如果请求头range具有值，0-2777，或者122-，或者134-1221112，则是浏览器恢复下载的断点续传请求。
                //响应码206，响应头content-range，包含本次下载的数据范围 134-1221111/1221112
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.PartialContent;
            }
            context.Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{fileLength}");
            //响应头增加accept-ranges，向浏览器声明服务端支持断点续传
            //相关必须的响应头，content-length,last-modified,etag，响应码200
            //浏览器需要的GMT的时间，用于last-modified,
            var gmtDateString = new DateTime(2021,1,1).ToString("r");
            //etag使用文件相关的特征值，例如把文件最后修改时间进行md5
            context.Response.Headers.Add("Accept-Ranges", "bytes");
            context.Response.Headers.Add("Content-Length", (end-start+1).ToString());
            context.Response.Headers.Add("Last-Modified", gmtDateString);
            var md5buffer= new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(fileName));
            var etag= System.BitConverter.ToString(md5buffer).Replace("-",string.Empty).ToLower();
            context.Response.Headers.Add("ETag", etag);
            for (long i = start; i < end; i++) {
                context.Response.OutputStream.WriteByte(32);
                context.Response.Flush();
            }
            //firefox,ie,经典edge支持关闭浏览器，重新打开后，在中断位置继续下载
            //高版本chrome不支持关闭浏览器后在中断位置继续下载。
            //但如果下载过程发生错误，比如网络中断，可以恢复在中断位置继续下载下载（chrome不关闭）；
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}