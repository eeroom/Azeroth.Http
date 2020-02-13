using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.NoIIS
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //需要在程序根目录下建立bin目录，把程序拷一份让asp.net加载
            //aspx文件用的codefile模式，不是codebehind模式
            var path = System.Environment.CurrentDirectory;
            SimpleHost msh = (SimpleHost)System.Web.Hosting.ApplicationHost.CreateApplicationHost(typeof(SimpleHost),"/",path);
            msh.ProcessRequest("TestHost.aspx");
            Console.ReadLine();
        }
    }

    public class SimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(string file)
        {
            System.Web.Hosting.SimpleWorkerRequest swr = new System.Web.Hosting.SimpleWorkerRequest(file, "", Console.Out);
            System.Web.HttpRuntime.ProcessRequest(swr);
        }
    }
}
