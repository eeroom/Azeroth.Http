using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    public class SoapClient<T> : System.Runtime.Remoting.Proxies.RealProxy
    {
        SoapHttpClientProtocol<T> SoapHttpClientProtocol { set; get; }
        SoapClient(string url) : base(typeof(T))
        {
            this.SoapHttpClientProtocol = new SoapHttpClientProtocol<T>();
            this.SoapHttpClientProtocol.Url = url;

        }

        public static T Create(string url)
        {
            return (T)new SoapClient<T>(url).GetTransparentProxy();
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage parameter)
        {
            //可以在这里做拦截
            var msg = parameter as System.Runtime.Remoting.Messaging.IMethodCallMessage;
            var rt = this.SoapHttpClientProtocol.GetInvoke().Invoke(msg.MethodName, msg.Args)[0];
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
