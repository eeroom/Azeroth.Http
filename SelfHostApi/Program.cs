using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
namespace KlzApi4SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new System.Web.Http.SelfHost.HttpSelfHostConfiguration("http://localhost:8086");
            config.Routes.MapHttpRoute("htt", "{controller}/{action}");

            using (var server = new System.Web.Http.SelfHost.HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
