using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.AspxTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.Environment.CurrentDirectory;
            SimpleHost msh = (SimpleHost)System.Web.Hosting.ApplicationHost.CreateApplicationHost(typeof(SimpleHost), "/", path);
            var fileName = "MailToBossTemplate.aspx";
            var ms = new System.IO.MemoryStream();
            var content = msh.ProcessRequest(fileName, ms);
            Console.WriteLine("通过模板生成的邮件内容");
            Console.WriteLine(content);
            Console.ReadKey();


        }
    }
}
