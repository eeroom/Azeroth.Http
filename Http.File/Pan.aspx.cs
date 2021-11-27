using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Http.File
{
    public partial class Pan : CmdPage<Pan>, IAnonymousPage
    {
        Model.HFDbContext dbcontext = new Model.HFDbContext();
        public object Upload(HttpContext context)
        {
            if (context.Request.Files.Count < 1)
                throw new ArgumentException("必须指定上传的文件内容");
            long Position = long.Parse(context.Request["Position"]);
            long FileSize = long.Parse(context.Request["FileSize"]);
            string fileId = context.Request["fileId"];
            string fullName = context.Request["WebkitRelativePath"];
            fullName = String.IsNullOrWhiteSpace(fullName) ? context.Request.Files[0].FileName : fullName;
            var rootFolder = context.Server.MapPath("");
            rootFolder = System.IO.Directory.GetParent(rootFolder).Parent.FullName;
            rootFolder = System.IO.Path.Combine(rootFolder, "Azeroth.File.UploadFiles");
            var filePath = System.IO.Path.Combine(rootFolder, fullName);
            Model.FileEntity fe = null;
            if (Position == 0)
            {
                if (System.IO.File.Exists(filePath))
                    throw new ArgumentException($"文件已经存在:{fullName}");
                fe = new Model.FileEntity()
                {
                    ClientHashValue = "md5",
                    FullName = fullName,
                    Size = FileSize,
                    UploadStepValue = Model.UploadStep.进行中
                };
                this.dbcontext.FileEntity.Add(fe);
            }
            var fileFolder = System.IO.Path.GetDirectoryName(filePath);
            if (!System.IO.Directory.Exists(fileFolder))
                System.IO.Directory.CreateDirectory(fileFolder);
            using (var filestream = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                filestream.Position = Position;
                context.Request.Files[0].InputStream.CopyTo(filestream);
                filestream.Flush(true);
                if (!string.IsNullOrEmpty(fileId))
                    context.Cache[fileId] = filestream.Position - 1;
                this.dbcontext.SaveChanges();
            }
            return new { msg = "ok",fe?.Id };
        }

        public object GetFileEntities(HttpContext context)
        {
            string path = context.Request["path"]??string.Empty;
            var lst = this.dbcontext.FileEntity.Where(x=>x.UploadStepValue== Model.UploadStep.完成).OrderByDescending(x => x.Id).ToList();
            var lstwrapper= lst.Select(x => Tuple.Create(x, System.IO.Path.GetDirectoryName(x.FullName))).ToList();
            var lstdir= lstwrapper.Select(x => x.Item2).Distinct().ToList();
            var lstdirAll= lstdir.Select(x => x.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => System.Linq.Enumerable.Range(1, x.Length).Select(a => string.Join("\\", x.Take(a))))
                .SelectMany(x => x)
                .Distinct()
                .ToList();
            var lstdirEntity= lstdirAll.Select(x => new Model.FileEntity()
            {
                FullName = x,
                 Id=int.MaxValue
            });
            var lstdirEntityWrapper= lstdirEntity.Select(x => Tuple.Create(x, System.IO.Path.GetDirectoryName(x.FullName)))
                .OrderBy(x=>x.Item2.Length)
                .ToList();
            lstdirEntityWrapper.AddRange(lstwrapper);
            if(!string.IsNullOrEmpty(path))
                lstdirEntityWrapper.Insert(0, Tuple.Create(new Model.FileEntity() { FullName = "..", Id = int.MaxValue }, path));
            var lstRT = lstdirEntityWrapper.Where(x => x.Item2 == path).Select(x=>x.Item1).Select(x => new
            {
                FullName= System.IO.Path.GetFileName(x.FullName),
                Path = x.Id == int.MaxValue ? (x.FullName==".."?System.IO.Path.GetDirectoryName(path):x.FullName) :string.Empty,
                Size = x.Size == 0 ? string.Empty : Math.Round((1.0 * x.Size / 1024 / 1024), 2, MidpointRounding.ToEven).ToString() + "MB",
                x.Id,
                CC = x.Id == int.MaxValue ? "dir" : "file"
            }).ToList();
            var rt = new{
                rows = lstRT,
                total = lstRT.Count
            };
            return rt;
        }

        public object GetUploadingFileEntities(HttpContext context)
        {
            
            var lst = this.dbcontext.FileEntity.Where(x=>x.UploadStepValue== Model.UploadStep.进行中).OrderByDescending(x => x.Id).ToList();
            var lstRt= lst.Select(x => new
            {
                x.FullName,
                Name=System.IO.Path.GetFileName(x.FullName),
                x.Id,
                x.Size,
                Position = long.Parse((context.Cache[x.Id.ToString()]?.ToString() ?? "1"))
            }).ToList();
            return lstRt;
        }

        public object Delete(HttpContext context)
        {
            DeleteFileInput deleteInput;
            using (var streamReader=new System.IO.StreamReader(context.Request.InputStream))
            {
                deleteInput= (DeleteFileInput)new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(streamReader.ReadToEnd(), typeof(DeleteFileInput));

            }
            var lstdd= deleteInput.lstId.Select(x => new Model.FileEntity() { Id = x }).ToList();
            lstdd.ForEach(x => {
                this.dbcontext.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            });
            this.dbcontext.SaveChanges();
            return new { msg = "ok" };
            
        }

        class DeleteFileInput
        {
            public int[] lstId { get; set; }
        }

        public object Complete(HttpContext context)
        {
            var fileid = context.Request["fileid"];
            var fe = new Model.FileEntity() { Id = int.Parse(fileid), UploadStepValue= Model.UploadStep.完成 };
            this.dbcontext.Configuration.ValidateOnSaveEnabled = false;
            var fewrapper= this.dbcontext.Entry(fe);
            fewrapper.State = System.Data.Entity.EntityState.Unchanged;
            fewrapper.Property(x => x.UploadStepValue).IsModified=true;
            this.dbcontext.SaveChanges();
            return new { msg = "ok" };
        }
    }
}