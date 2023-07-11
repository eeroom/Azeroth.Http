using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxTemplateEngine
{
    class App
    {
        static void Main(string[] args)
        {
            //需要在程序根目录下建立bin目录，把程序拷一份让asp.net加载，增加编译后期事件，自动执行拷贝
            //mkdir $(TargetDir)\bin
            //copy $(TargetPath) $(TargetDir)\bin\
            //aspx文件用的codefile模式，不是codebehind模式
            var path = System.Environment.CurrentDirectory;
            SimpleLocalServerHost msh = (SimpleLocalServerHost)System.Web.Hosting.ApplicationHost.CreateApplicationHost(typeof(SimpleLocalServerHost), "/", path);

            while (true)
            {
                System.Console.WriteLine("按任意键，执行Email.aspx模板渲染，并且得到渲染后的内容");
                Console.ReadKey();
                var fileName = "Email.aspx";
                using (var ms = new System.IO.MemoryStream())
                {
                    msh.ProcessRequest(fileName, "", ms);
                    ms.Position = 0;
                    var reader = new System.IO.StreamReader(ms);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }

           
           
        }
    }
}
