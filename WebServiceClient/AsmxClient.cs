﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceClient
{
    public class AsmxClient<T> : System.Runtime.Remoting.Proxies.RealProxy
    {
        SoapHttpClientProtocolWithInterceptor soapHttpClient;

        AsmxClient(string url) : base(typeof(T))
        {
            this.soapHttpClient = new SoapHttpClientProtocolWithInterceptor();
            this.soapHttpClient.Url = url;
            
        }

        public static T Create(string url)
        {
            var client = new AsmxClient<T>(url);
            return (T)client.GetTransparentProxy();
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage parameter)
        {
            var msg = parameter as System.Runtime.Remoting.Messaging.IMethodCallMessage;
            var rt = this.soapHttpClient.SendRequest(msg.MethodName, msg.Args);
            var rtmsg = new System.Runtime.Remoting.Messaging.ReturnMessage(rt, null, 0, msg.LogicalCallContext, msg);
            return rtmsg;
        }

        [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
        public class SoapHttpClientProtocolWithInterceptor : System.Web.Services.Protocols.SoapHttpClientProtocol
        {
            static SoapHttpClientProtocolWithInterceptor()
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
                AddToCache(typeof(AsmxClient<T>.SoapHttpClientProtocolWithInterceptor), sct);
            }
            public object SendRequest(string methodName, object[] parameters)
            {
                return this.Invoke(methodName, parameters)[0];
            }
        }
    }
}