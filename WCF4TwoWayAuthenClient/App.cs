using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WcfTwoWayAuthenticationClient
{
    class App
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (x, y, z, a) => true;
            using (var factory = new System.ServiceModel.Web.WebChannelFactory<IHome>("wch"))
            {
               var client = factory.CreateChannel();

               var rt = client.DoWork();
            }
        }
    }

    [ServiceContract]
    public interface IHome
    {
        [OperationContract]
        string DoWork();
    }

}
