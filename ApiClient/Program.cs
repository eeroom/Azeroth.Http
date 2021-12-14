using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient {
    class Program {
        static void Main(string[] args) {
            var urlHome = System.Configuration.ConfigurationManager.AppSettings["urlHome"];
            var client = SoapClient<IHome>.Create(urlHome);
            var rt = client.HelloWorld(3);
            Console.WriteLine(rt);
            Console.ReadLine();
        }
    }
}
