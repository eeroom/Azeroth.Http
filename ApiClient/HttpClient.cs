using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ApiClient
{
    public class HttpClient: System.Runtime.Remoting.Proxies.RealProxy
    {
        /// <summary>
        /// 通过meta的特性，获取服务基址
        /// </summary>
        Type meta;
        HttpClient(Type meta):base(meta)
        {
            this.meta = meta;
        }
        public static T Create<T>()
        {
            var client = new HttpClient(typeof(T));
            return (T)client.GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            //通过method的元数据获取api方法的一些信息，调用方式get,post，请求值：表单/json/xml ，返回值：json/xml
            throw new NotImplementedException();
        }
    }
}
