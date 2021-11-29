using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Http.File
{
    public partial class wangpan : CmdPage<wangpan>, IAnonymousPage
    {
        Model.HFDbContext dbcontext = new Model.HFDbContext();

        string GetRootFolder(HttpContext context)
        {
            var rootFolder = context.Server.MapPath("");
            var rootFolder2 = System.IO.Directory.GetParent(rootFolder).Parent.FullName;
            var rootFolder3 = System.IO.Path.Combine(rootFolder2, "Azeroth.File.UploadFiles");
            return rootFolder3;
        }
        public object Upload(HttpContext context)
        {
            if (context.Request.Files.Count < 1)
                throw new ArgumentException("必须指定上传的文件内容");
            long Position = long.Parse(context.Request["Position"]);
            long FileSize = long.Parse(context.Request["FileSize"]);
            string fileId = context.Request["fileId"];
            string fullName = context.Request["WebkitRelativePath"];
            fullName = String.IsNullOrWhiteSpace(fullName) ? context.Request.Files[0].FileName : fullName;
            var rootFolder = this.GetRootFolder(context);
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
            var lst = this.dbcontext.FileEntity.AsNoTracking().Where(x=>x.UploadStepValue== Model.UploadStep.完成).OrderByDescending(x => x.Id).ToList();
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
                lstdirEntityWrapper.Insert(0, Tuple.Create(new Model.FileEntity() { FullName = "..", Id = int.MinValue }, path));
            var lstRT = lstdirEntityWrapper.Where(x => x.Item2 == path).Select(x=>x.Item1).Select(x => new
            {
                FullName= System.IO.Path.GetFileName(x.FullName),
                Path = x.Id == int.MaxValue ? (x.FullName==".."?System.IO.Path.GetDirectoryName(path):x.FullName) :string.Empty,
                Size = x.Size == 0 ? string.Empty : Math.Round((1.0 * x.Size / 1024 / 1024), 2, MidpointRounding.ToEven).ToString() + "MB",
                x.Id,
                CC = x.Id == int.MaxValue ? "dir" : (x.Id==int.MinValue? "parent" : "file")
            }).ToList();
            var rt = new{
                rows = lstRT,
                total = lstRT.Count
            };
            return rt;
        }

        public object GetUploadingFileEntities(HttpContext context)
        {
            
            var lst = this.dbcontext.FileEntity.AsNoTracking().Where(x=>x.UploadStepValue== Model.UploadStep.进行中).OrderByDescending(x => x.Id).ToList();
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
            var rootFolder = this.GetRootFolder(context);
            //删除文件夹
            var lstdddir= deleteInput.lstfe.Where(x => x.cc == "dir").Select(x => x.path).Distinct().ToList();
            lstdddir.ForEach(x =>
            {
                var fullpath = System.IO.Path.Combine(rootFolder, x);
                if(System.IO.Directory.Exists(fullpath))
                    System.IO.Directory.Delete(fullpath, true);//删磁盘目录
            });
            //删除文件
            var lstId = deleteInput.lstfe.Where(x => x.cc == "file").Select(x=>x.id);
            var lstddfile = this.dbcontext.FileEntity.Where(x => lstId.Contains(x.Id)).ToList();
            lstddfile.ForEach(x =>
            {

                var fullpath = System.IO.Path.Combine(rootFolder, x.FullName);
                if(System.IO.File.Exists(fullpath))
                    System.IO.File.Delete(fullpath);//删磁盘文件
            });
            var lstddfile2= lstdddir.Select(x => x.Replace("\\","/") + "/")
                .Select(x => this.dbcontext.FileEntity.Where(a => a.FullName.StartsWith(x)).ToList())
                .SelectMany(x => x).ToList();
            this.dbcontext.FileEntity.RemoveRange(lstddfile);//删数据库记录
            this.dbcontext.FileEntity.RemoveRange(lstddfile2);
            this.dbcontext.SaveChanges();
            return new { msg = "ok" };
            
        }

        class DeleteFileInput
        {
            public List<Fe> lstfe { get; set; }
        }

        class Fe
        {
            public int id { get; set; }

            public string path { get; set; }

            public string cc { get; set; }
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

        public void Download(HttpContext context)
        {
            var fileids= context.Request["fileids"];
            var lstfileid = fileids.Split(',').Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x))
                .ToList();
            if (lstfileid.Count < 1)
                return;
            var lstFileEntity = this.dbcontext.FileEntity.Where(x =>lstfileid.Contains(x.Id)).ToList();
            if (lstFileEntity.Count != lstfileid.Count)
                throw new ArgumentException("指定的文件数据库记录不存在,文件id:" + lstfileid[0]);
            if (lstFileEntity.Count == 1)
            {
                var fileEntity = lstFileEntity[0];
                if (fileEntity == null)
                    throw new ArgumentException("指定的文件数据库记录不存在,文件id:" + lstfileid[0]);
                context.Response.ContentType = "application/octet-stream";
                //这涉及RFC标准
                context.Response.AddHeader("Content-Disposition", "attachment;filename*=utf-8'zh_cn'" + context.Server.UrlEncode(System.IO.Path.GetFileName(fileEntity.FullName)));
                long start = 0;
                long end = fileEntity.Size - 1;
                var range = context.Request.Headers["Range"] ?? string.Empty;
                var rangeFlag = "bytes=";
                if (range.StartsWith(rangeFlag))
                {
                    var lstRange = range.Substring(rangeFlag.Length).Split(new char[] { '-', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (lstRange.Count == 1)
                    {
                        start = long.Parse(lstRange.First());
                    }
                    else if (lstRange.Count == 2)
                    {
                        start = long.Parse(lstRange.First());
                        end = long.Parse(lstRange.Last());
                    }
                    else if (lstRange.Count > 2)
                    {
                        throw new ArgumentException("服务端不支持多个range范围的下载请求");
                    }
                    //如果请求头range具有值，0-2777，或者122-，或者134-1221112，则是浏览器恢复下载的断点续传请求。
                    //响应码206，响应头content-range，包含本次下载的数据范围 134-1221111/1221112
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.PartialContent;
                }
                context.Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{fileEntity.Size}");
                //响应头增加accept-ranges，向浏览器声明服务端支持断点续传
                //相关必须的响应头，content-length,last-modified,etag，响应码200
                //浏览器需要的GMT的时间，用于last-modified,
                var gmtDateString = new DateTime(2021, 1, 1).ToString("r");
                //etag使用文件相关的特征值，例如把文件最后修改时间进行md5
                context.Response.Headers.Add("Accept-Ranges", "bytes");
                context.Response.Headers.Add("Content-Length", (end - start + 1).ToString());
                context.Response.Headers.Add("Last-Modified", gmtDateString);
                var md5buffer = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(fileEntity.FullName));
                var etag = System.BitConverter.ToString(md5buffer).Replace("-", string.Empty).ToLower();
                context.Response.Headers.Add("ETag", etag);
                int bufferSize = 8 * 1024;
                var rootFolder = this.GetRootFolder(context);
                using (var fs=new System.IO.FileStream(System.IO.Path.Combine(rootFolder,fileEntity.FullName), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                {
                    fs.Position = start;
                    var buffer = new byte[bufferSize];
                    var datalength = 0;
                    while ((datalength=fs.Read(buffer,0,bufferSize))>0)
                    {
                        context.Response.OutputStream.Write(buffer, 0, datalength);
                        context.Response.Flush();
                    }
                }
                //firefox,ie,经典edge支持关闭浏览器，重新打开后，在中断位置继续下载
                //高版本chrome不支持关闭浏览器后在中断位置继续下载。
                //但如果下载过程发生错误，比如网络中断，可以恢复在中断位置继续下载下载（chrome不关闭）；
            }
            else
            {
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachment;filename*=utf-8'zh_cn'" + context.Server.UrlEncode(System.IO.Path.GetFileName(lstFileEntity[0].FullName)+"-zip.zip"));
                ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream =
                    new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(context.Response.OutputStream);
                zipStream.SetLevel(6);
                string rootPath = this.GetRootFolder(context);
                var filePaths = lstFileEntity.Select(x=>System.IO.Path.Combine(rootPath,x.FullName)).ToList();
                foreach (var filePath in filePaths)
                {
                    string entryName = filePath.Replace(rootPath, string.Empty).TrimStart('\\');
                    zipStream.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry(entryName));
                    using (var fs = System.IO.File.Open(filePath, System.IO.FileMode.Open))
                    {
                        var bufferSize = 8 * 1024;
                        var buffer = new byte[bufferSize];
                        int length = -1;
                        while ((length = fs.Read(buffer, 0, bufferSize)) > 0)
                        {
                            zipStream.Write(buffer, 0, length);
                            context.Response.Flush();
                        }
                    }
                }
                zipStream.Finish();
                context.Response.End();
            }
        }
    }
}