using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.Http.Util
{
    /// <summary>
    /// Download02 的摘要说明
    /// </summary>
    public class Download02 : IHttpHandler
    {
        /// <summary>
        /// 边打包边下载，
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=Azeroth.Http.Util.zip");
            
            ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = 
                new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(context.Response.OutputStream);
            zipStream.SetLevel(6);
            string rootPath = System.Web.HttpRuntime.BinDirectory;
            var filePaths = System.IO.Directory.GetFiles(rootPath, "*", System.IO.SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                string entryName = filePath.Replace(rootPath, string.Empty).TrimStart('\\');
                zipStream.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry(entryName));
                using (var fs=System.IO.File.Open(filePath, System.IO.FileMode.Open))
                {
                    var bufferSize = 80 * 1024;
                    var buffer = new byte[bufferSize];
                    int length = -1;
                    while ((length=fs.Read(buffer,0, bufferSize))>0)
                    {
                        zipStream.Write(buffer, 0, length);
                        context.Response.Flush();
                    }
                }
            }
            zipStream.Finish();
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