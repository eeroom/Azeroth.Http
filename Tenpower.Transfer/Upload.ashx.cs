using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Http.File
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count < 1)
                throw new ArgumentException("必须指定上传的文件内容");
            long Position = long.Parse(context.Request["Position"]);
            long FileSize = long.Parse(context.Request["FileSize"]);
            string fullName = context.Request["WebkitRelativePath"];
            fullName = String.IsNullOrWhiteSpace(fullName) ? context.Request.Files[0].FileName : fullName;
            var rootFolder= context.Server.MapPath("/UploadFiles");
            var filePath = System.IO.Path.Combine(rootFolder, fullName);
            var fileFolder = System.IO.Path.GetDirectoryName(filePath);
            if (!System.IO.Directory.Exists(fileFolder))
                System.IO.Directory.CreateDirectory(fileFolder);
            
            using (var filestream=new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
            {
                filestream.Position = Position;
                context.Request.Files[0].InputStream.CopyTo(filestream);
                filestream.Flush();
                if (filestream.Length == FileSize)
                    context.Response.Write("ok");
                else if (filestream.Length < FileSize)
                    context.Response.Write("ing");
                else
                    context.Response.Write("代码逻辑错误，服务端文件的大小超过浏览器端文件大小");
            }
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