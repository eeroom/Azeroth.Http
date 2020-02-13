using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.ASPXEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //需要在程序根目录下建立bin目录，把程序拷一份让asp.net加载
            //aspx文件用的codefile模式，不是codebehind模式
            var path = System.Environment.CurrentDirectory;
            SimpleHost msh = (SimpleHost)System.Web.Hosting.ApplicationHost.CreateApplicationHost(typeof(SimpleHost), "/", path);
            using (var ls=new System.Net.HttpListener())
            {
                ls.Prefixes.Add("http://localhost:8054/");
                ls.Start();
                while (true)
                {
                    var context= ls.GetContext();
                    context.Response.ContentType = "text/html; Charset=UTF-8";
                    context.Response.StatusCode = 200;
                    System.Threading.ThreadPool.QueueUserWorkItem(x=>msh.ProcessRequest(context.Request.Url,context.Response.OutputStream));
                }
            }
        }
    }

    public class SimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(Uri url,System.IO.Stream stream)
        {
            string file= url.LocalPath.Trim('/');
            using (var wt=new System.IO.StreamWriter(stream))
            {
                System.Web.Hosting.SimpleWorkerRequest swr = new System.Web.Hosting.SimpleWorkerRequest(file, "", wt);
                System.Web.HttpRuntime.ProcessRequest(swr);
            }
        }
    }
}
