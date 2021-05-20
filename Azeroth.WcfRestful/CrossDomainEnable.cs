using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestful
{
    public class CrossDomainEnable:ICrossDomainEnable
    {
        public CrossDomainEnable()
        {
            var context = System.ServiceModel.Web.WebOperationContext.Current;
            //每次的响应都需要添加这个响应头，否则js取不到响应的数据
            //*号不能传cookie
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            var headerOrigin = context.IncomingRequest.Headers["Origin"];
            context.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", headerOrigin);
        }
        public void OptionsHandler()
        {
            //CORS协议的约定
            var context = System.ServiceModel.Web.WebOperationContext.Current;
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            context.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT");
            context.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
            //这个allow header，高版本火狐有x-request-header
            //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "content-type");
            string headerAllow = context.IncomingRequest.Headers["Access-Control-Request-Headers"];
            context.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", headerAllow);

        }
    }
}
