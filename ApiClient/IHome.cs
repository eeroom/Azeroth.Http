using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
    public interface IHome
    {
        [System.Web.Services.Protocols.SoapDocumentMethod]
        string HelloWorld(int a);
    }
}
