using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlzClient
{
    public class SoapChannelFactory<T> : System.Runtime.Remoting.Proxies.RealProxy
    {
        SoapHttpClientProtocol<T> SoapHttpClientProtocol { set; get; }
        SoapChannelFactory(string url) : base(typeof(T))
        {
            this.SoapHttpClientProtocol = new SoapHttpClientProtocol<T>();
            this.SoapHttpClientProtocol.Url = url;

        }

        public static T CreateChannel(string url)
        {
            return (T)new SoapChannelFactory<T>(url).GetTransparentProxy();
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage parameter)
        {
            //可以在这里做拦截
            var msg = parameter as System.Runtime.Remoting.Messaging.IMethodCallMessage;
            try
            {
                var rt = this.SoapHttpClientProtocol.GetInvoke().Invoke(msg.MethodName, msg.Args)[0];
                var rtmsg = new System.Runtime.Remoting.Messaging.ReturnMessage(rt, null, 0, msg.LogicalCallContext, msg);
                return rtmsg;
            }
            catch (Exception ex)
            {
                //这里把异常重新包装一下，包含参数和url信息,这个样程序在其全局处理异常的地方，就可以针对这个特定异常类型进行处理，获取到请求参数和url等信息
                //如果不这么处理，那么在程序在其全局异常处理的地方拿不到请求时的关键信息，虽然记录了异常信息，但确实最关键了，不好后续使用相同参数去复现
                //如果在这里直接记录异常信息，也能把参数，url等信息记录到，但是没必要，这里作为类库方，只是把异常信息尽量明确就好，记录日志的逻辑交给调用方去处理
                throw new InvokedException("调用远程webservice接口发生异常", ex) { Url = this.SoapHttpClientProtocol.Url, Action = msg.MethodName, Parameter = msg.Args };
            }
           
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
