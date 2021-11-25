using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Http.File
{
    public partial class FileExplorer : CmdPage<FileExplorer>, IAnonymousPage
    {
        Model.HFDbContext dbcontext = new Model.HFDbContext();
        public object Upload(HttpContext context)
        {
            if (context.Request.Files.Count < 1)
                throw new ArgumentException("必须指定上传的文件内容");
            long Position = long.Parse(context.Request["Position"]);
            long FileSize = long.Parse(context.Request["FileSize"]);
            string fullName = context.Request["WebkitRelativePath"];
            fullName = String.IsNullOrWhiteSpace(fullName) ? context.Request.Files[0].FileName : fullName;
            var rootFolder = context.Server.MapPath("");
            rootFolder = System.IO.Directory.GetParent(rootFolder).Parent.FullName;
            rootFolder = System.IO.Path.Combine(rootFolder, "Azeroth.File.UploadFiles");
            var filePath = System.IO.Path.Combine(rootFolder, fullName);
            var fileFolder = System.IO.Path.GetDirectoryName(filePath);
            if (!System.IO.Directory.Exists(fileFolder))
                System.IO.Directory.CreateDirectory(fileFolder);
            using (var filestream = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                filestream.Position = Position;
                context.Request.Files[0].InputStream.CopyTo(filestream);
                filestream.Flush(true);
            }
            return new { msg = "ok" };
        }

        public List<Model.FileEntity> GetFileEntities(HttpContext context)
        {
            var lst = this.dbcontext.FileEntity.OrderByDescending(x => x.Id).ToList();
            return lst;
        }
    }
}