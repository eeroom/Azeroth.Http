using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hg.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:50682/Home.asmx";
            var home= (IHome)new AsmxClientProxy<IHome>(url).GetTransparentProxy();
            var rt= home.HelloWorld(3);

            var home2 = (IHome2)new AsmxClientProxy<IHome2>(url).GetTransparentProxy();
            var rt2 = home2.HelloWorld(3);

            var home3 = (IHome)new AsmxClientProxy<IHome>(url).GetTransparentProxy();
            var rt3 = home3.HelloWorld(3);
        }
    }

    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
     public interface IHome
    {
        [System.Web.Services.Protocols.SoapDocumentMethod]
        string HelloWorld(int parameter);
    }

    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
    public interface IHome2
    {
        [System.Web.Services.Protocols.SoapDocumentMethod]
        string HelloWorld(int parameter);
    }
}
