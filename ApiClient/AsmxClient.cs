using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    public class AsmxClient : System.Runtime.Remoting.Proxies.RealProxy
    {
        Func<string, object[], object[]> SoapHttpClientInvoke { set; get; }

        AsmxClient(Type meta) : base(meta)
        {
       

        }

        public static T Create<T>(string url)
        {
            var soapClient = new SoapHttpClientProtocol<T>();
            soapClient.Url = url;
            var client = new AsmxClient(typeof(T));
            client.SoapHttpClientInvoke = soapClient.GetInvoke();
            return (T)client.GetTransparentProxy();
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage parameter)
        {
            //可以在这里做拦截
            var msg = parameter as System.Runtime.Remoting.Messaging.IMethodCallMessage;
            var rt = this.SoapHttpClientInvoke(msg.MethodName, msg.Args)[0];
            var rtmsg = new System.Runtime.Remoting.Messaging.ReturnMessage(rt, null, 0, msg.LogicalCallContext, msg);
            return rtmsg;
        }
    }


    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
    class SoapHttpClientProtocol<T> : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        static SoapHttpClientProtocol()
        {
            var assm = System.AppDomain.CurrentDomain.GetAssemblies()
                .Cast<System.Reflection.Assembly>()
                .FirstOrDefault(x => x.FullName.IndexOf("System.Web.Services,") == 0);
            if (assm == null)
                throw new Exception("未找到程序集System.Web.Services");
            var sctMeta = assm.GetType("System.Web.Services.Protocols.SoapClientType", true);
            var meta = typeof(T);
            var sct = System.Activator.CreateInstance(sctMeta,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new object[] { meta },
                System.Globalization.CultureInfo.CurrentCulture);
            AddToCache(typeof(SoapHttpClientProtocol<T>), sct);
        }


        internal Func<string, object[], object[]> GetInvoke()
        {
            return this.Invoke;
        }
    }
}
