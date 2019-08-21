using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Azeroth.WebApi
{
    public class HaishanActionFilter : System.Web.Http.Filters.IActionFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var response= await continuation();
            var content= response.Content as ObjectContent;
            if (content == null)
                return response;
            var data= content.Value as ResultDataWrapper;
            if (data == null)
                return response;
            if (!string.IsNullOrEmpty(data.token))
                return response;//登陆api不要再进行处理
            var userInfoWrapper= (UserInfoWrapper)actionContext.Request.GetDependencyScope().GetService(typeof(UserInfoWrapper));
            if (userInfoWrapper.JwtData == null)
                return response;//不涉及token的api
            if (!userInfoWrapper.JwtData.TokenIsOld())
                return response;//没到滑动更新token的阈值
            data.SetToken(JwtHelper.Encode(userInfoWrapper.JwtData.SlidingTimeout()));
            return response;
        }
    }
}