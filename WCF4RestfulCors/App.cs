using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCF4RestfulCors
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在启动api服务");
            var lsthost = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(CrossDomainEnable))).Select(x => new System.ServiceModel.Web.WebServiceHost(x)).ToList();
            lsthost.ForEach(x => x.Open());
            Console.WriteLine("api服务正在运行");
            Console.WriteLine("服务地址请查看app.config中的终结点配置");
            Console.WriteLine("----------------");
            Console.ReadKey();
            lsthost.ForEach(x => x.Close());
        }
    }
}
