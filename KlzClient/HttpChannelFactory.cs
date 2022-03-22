using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace KlzClient
{
    public class HttpChannelFactory<T> : System.Runtime.Remoting.Proxies.RealProxy
    {
        string baseurl { set; get; }
 
        HttpChannelFactory(string baseurl) : base(typeof(T))
        {
            this.baseurl = baseurl.TrimEnd('/');
        }
        public static T CreateChannel(string baseurl)
        {
            return (T)new HttpChannelFactory<T>(baseurl).GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage parameter)
        {
            var msg = (System.Runtime.Remoting.Messaging.IMethodCallMessage)parameter;
            var rm = msg.MethodBase.GetCustomAttributes(typeof(RequestMappingAttribute), false).Cast<RequestMappingAttribute>()
              .FirstOrDefault() ?? RequestMappingAttribute.Default;
            var url = this.GetUrl(msg, rm);
            try
            {
                byte[] payload = this.ParseParameter(msg, rm);
                if (rm.Method == HttpMetod.GET)
                    url = $"{url}?{System.Text.Encoding.UTF8.GetString(payload)}";
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = rm.Method.ToString();
                if (rm.Method == HttpMetod.POST)
                {
                    request.ContentType = this.ParseContentType(rm.Consumes);
                    request.GetRequestStream().Write(payload, 0, payload.Length);
                }
                var methodInfo = (System.Reflection.MethodInfo)msg.MethodBase;
                if (typeof(System.IO.Stream).IsAssignableFrom(methodInfo.ReturnType))
                    return new System.Runtime.Remoting.Messaging.ReturnMessage(request.GetResponse().GetResponseStream(), null, 0, msg.LogicalCallContext, msg);
                using (var reader = new System.IO.StreamReader(request.GetResponse().GetResponseStream()))
                {
                    if (typeof(void) == methodInfo.ReturnType)
                        return new System.Runtime.Remoting.Messaging.ReturnMessage(null, null, 0, msg.LogicalCallContext, msg);
                    var valuestr = reader.ReadToEnd();
                    var value = this.ParseResult(methodInfo, rm, valuestr);
                    return new System.Runtime.Remoting.Messaging.ReturnMessage(value, null, 0, msg.LogicalCallContext, msg);
                }
            }
            catch (Exception ex)
            {
                //这里把异常重新包装一下，包含参数和url信息,这个样程序在其全局处理异常的地方，就可以针对这个特定异常类型进行处理，获取到请求参数和url等信息
                //如果不这么处理，那么在程序在其全局异常处理的地方拿不到请求时的关键信息，虽然记录了异常信息，但确实最关键了，不好后续使用相同参数去复现
                //如果在这里直接记录异常信息，也能把参数，url等信息记录到，但是没必要，这里作为类库方，只是把异常信息尽量明确就好，记录日志的逻辑交给调用方去处理
                throw new InvokedException("调用远程api发生异常", ex) { Url = url, Parameter = msg.Args };
            }
            
            
        }

        private object ParseResult(System.Reflection.MethodInfo methodInfo, RequestMappingAttribute rm, string valuestr)
        {
            switch (rm.Produces)
            {
                case MediaType.Json:
                    return Newtonsoft.Json.JsonConvert.DeserializeObject(valuestr, methodInfo.ReturnType);
                case MediaType.FormUrlEncoded:
                    throw new NotImplementedException();
                case MediaType.Xml:
                    return this.ParseFromXml(methodInfo.ReturnType, valuestr);
                case MediaType.FormData:
                    throw new NotImplementedException();
                case MediaType.Text:
                    return valuestr;
                default:
                    throw new NotImplementedException();
            }
        }

        private object ParseFromXml(Type targetType, string valuestr)
        {
            var xmlserializer = new System.Xml.Serialization.XmlSerializer(targetType);
            using (var reader=new System.IO.StringReader(valuestr))
            {
                return xmlserializer.Deserialize(reader);
            }
        }

        private string ParseContentType(MediaType consumes)
        {
            switch (consumes)
            {
                case MediaType.Json:
                    return "application/json";
                case MediaType.FormUrlEncoded:
                    return "application/x-www-form-urlencoded";
                case MediaType.Xml:
                    return "appliction/xml";
                case MediaType.FormData:
                    throw new NotImplementedException();
                case MediaType.Text:
                    return "text/plain";
                default:
                    throw new ArgumentException();
            }
        }

        private byte[] ParseParameter(IMethodCallMessage msg, RequestMappingAttribute rm)
        {
            switch (rm.Consumes)
            {
                case MediaType.Json:
                    return System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(msg.Args[0]));
                case MediaType.FormUrlEncoded:
                    var lstPmeta = msg.MethodBase.GetParameters().Select(x => Tuple.Create(x.Name, x.ParameterType.Assembly.FullName.ToLower())).ToList();
                    var lstPstr = msg.Args.Zip(lstPmeta, (pvalue, pmeta) => this.FlatParameter(pvalue, pmeta))
                        .SelectMany(x => x.Select(a => $"{a.Key}={a.Value}"))
                        .ToList();
                    return System.Text.Encoding.UTF8.GetBytes(string.Join("&", lstPstr));
                case MediaType.Xml:
                    return this.ParseToXml(msg.Args[0]);
                case MediaType.FormData:
                    throw new NotImplementedException();
                case MediaType.Text:
                    return System.Text.Encoding.UTF8.GetBytes(msg.Args[0]?.ToString());
                default:
                    throw new ArgumentException("不支持请求参数类型");
            }
        }

        private byte[] ParseToXml(object value)
        {
            var xmlserializer=new System.Xml.Serialization.XmlSerializer(value.GetType());
            using (var ms=new System.IO.MemoryStream())
            {
                xmlserializer.Serialize(ms, value);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        static string mscorlibName = "mscorlib,";
        /// <summary>
        /// 把参数展平，e.g:一个自定义class,展成字典结构，然后转成：name=3&age=4
        /// </summary>
        /// <param name="pvalue"></param>
        /// <param name="pmeta"></param>
        /// <returns></returns>
        private Dictionary<string,string> FlatParameter(object pvalue, Tuple<string,string> pmeta)
        {
            if(pmeta.Item2.StartsWith(mscorlibName))
                return new Dictionary<string, string>() { { pmeta.Item1, pvalue.ToString() } };
            return Newtonsoft.Json.Linq.JToken.FromObject(pvalue).Cast<Newtonsoft.Json.Linq.JProperty>().ToDictionary(x => x.Name, x => x.Value.ToString());
        }

        private string GetUrl(IMethodCallMessage msg, RequestMappingAttribute rm)
        {
            if (rm.Action != null)
                return $"{this.baseurl}/{rm.Action}";
            return $"{this.baseurl}/{msg.MethodBase.DeclaringType.Name.ToLower()}/{msg.MethodName.ToLower()}";
        }
    }
}
