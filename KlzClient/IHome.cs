using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpClient
{
    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
    public interface IHome
    {
        [System.Web.Services.Protocols.SoapDocumentMethod]
        string HelloWorld(int a);
    }
}
