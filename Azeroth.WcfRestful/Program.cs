using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestful
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在启动api服务");
            var lsthost = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(CrossDomainEnable))).Select(x => new System.ServiceModel.Web.WebServiceHost(x)).ToList();
            lsthost.ForEach(x => x.Open());
            Console.WriteLine("api服务正在运行");
            Console.WriteLine("----------------");
            Console.ReadKey();
            lsthost.ForEach(x => x.Close());
        }
    }
}
