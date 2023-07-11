using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpClient {
    class App {
        static void Main(string[] args) {
            var urlHome = System.Configuration.ConfigurationManager.AppSettings["urlHome"];
            var client = SoapChannelFactory<IHome>.CreateChannel(urlHome);
            var rt = client.HelloWorld(3);

            var urlaccount = "http://localhost:7719";
            var aclient = HttpChannelFactory<IAccount>.CreateChannel(urlaccount);
            ApiData<bool> rt2= aclient.Login(new LoginDTO() { Name = "张三", Pwd = "123456" });

            Console.WriteLine(rt);
            Console.ReadLine();
        }
    }
}
