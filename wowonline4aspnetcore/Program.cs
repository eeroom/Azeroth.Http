using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wowonline4aspnetcore
{
    class Program {
        static void Main(string[] args) {
            string baseAddress = "http://localhost:8016/";
            // 启动 OWIN host
            Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: baseAddress);
            Console.WriteLine("程序已启动,按任意键退出");
            Console.ReadLine();
        }
    }
}
