using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceClient
{
    public class SoapHttpClientProtocolIntercept: System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        protected new object[] Invoke(string methodName, object[] parameters)
        {
            //拦截调用，记录异常情况的参数等，接口调用耗时记录等
            try
            {
               return base.Invoke(methodName, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}