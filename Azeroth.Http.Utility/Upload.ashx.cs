using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.Http.Utility
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
            int start = int.Parse(context.Request["start"]);
            int size = int.Parse(context.Request["size"]);
            var fileFolder= context.Server.MapPath("/uploadData");
            if (!System.IO.Directory.Exists(fileFolder))
                System.IO.Directory.CreateDirectory(fileFolder);
            var tempFileName = System.IO.Path.Combine(fileFolder, "_temp_" + context.Request.Files[0].FileName);
            using (var filestream=new System.IO.FileStream(tempFileName, System.IO.FileMode.OpenOrCreate))
            {
                filestream.Position = start;
                context.Request.Files[0].InputStream.CopyTo(filestream);
                filestream.Flush();
                if (filestream.Length >= size)
                {
                    filestream.Close();
                    System.IO.File.Move(tempFileName, System.IO.Path.Combine(fileFolder, context.Request.Files[0].FileName));
                }
            }
            
            context.Response.Write("hello world");
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