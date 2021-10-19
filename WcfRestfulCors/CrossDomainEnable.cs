using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestfulCors
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
            //CORS协议的约定,关键响应头，
            //Access-Control-Allow-Origin  必选，且后续每次响应都需要，这个值要么是请求头里面的Origin对应的值，要么为*，高版本浏览器对*不友好，有额外限制
            //Access-Control-Allow-Methods 必选
            //Access-Control-Max-Age   必须，单位为秒
            //Access-Control-Allow-Headers 可选，如果请求头有Access-Control-Request-Headers，则必须，可以多个，逗号分隔，不限于预检请求头中Access-Control-Request-Headers的值
            //Access-Control-Allow-Credentials 可选，影响是否发送cookie和http认证信息和浏览器是否允许js读取响应中的cookie,要么值为true,要么没有这个响应头，不存在false；配合这个功能，浏览器的xhr对象的.withCredentials属性设置为true
            //Access-Control-Expose-Headers 可选，浏览器默认只能拿到6个响应头的值，如果需要额外的，就把额外的响应头名称在这里知道
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
