using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.Http.Utility
{
    /// <summary>
    /// Dowanload 的摘要说明
    /// </summary>
    public class Dowanload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/octet-stream";
            //这涉及RFC标准
            context.Response.AddHeader("Content-Disposition", "attachment;filename*=utf-8'zh_cn'"+context.Server.UrlEncode("测试文件a下载1.txt"));
            using (var fs=new System.IO.FileStream(context.Server.MapPath("Readme.html"), System.IO.FileMode.Open))
            {
                fs.CopyTo(context.Response.OutputStream);
            }
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}