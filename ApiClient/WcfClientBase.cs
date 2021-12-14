using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ApiClient
{
    /// <summary>
    /// 使用场景，老项目已经使用工具生成了客户端代码，但需要增加异常处理，超时检测等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WcfClientBase<T> : System.ServiceModel.ClientBase<T> where T : class
    {

        public WcfClientBase()
        {
        }

        public WcfClientBase(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public WcfClientBase(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public WcfClientBase(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public WcfClientBase(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        protected override T CreateChannel()
        {
            var tmp = base.CreateChannel();
            var wrapper = new WcfChannelWrapper(tmp);
            return (T)wrapper.GetTransparentProxy();
        }

        class WcfChannelWrapper : System.Runtime.Remoting.Proxies.RealProxy
        {
            object wcfChannel;
            public WcfChannelWrapper(object wcfChannel) : base(typeof(T))
            {
                this.wcfChannel = wcfChannel;
            }

            public override IMessage Invoke(IMessage msg)
            {
                //拦截调用，记录异常情况的参数等，接口调用耗时记录等
                try
                {
                    var proxy = System.Runtime.Remoting.RemotingServices.GetRealProxy(this.wcfChannel);
                    return proxy.Invoke(msg);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
