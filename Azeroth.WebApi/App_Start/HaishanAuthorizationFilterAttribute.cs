using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Net.Http;
namespace Azeroth.WebApi
{
    public class HaishanAuthorizationFilterAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.FirstOrDefault(x => x.Key == "auth").Value;
            authHeader = authHeader ?? new List<string>();
            string token = authHeader.FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                var data = ResultDataWrapper.Create(System.Net.HttpStatusCode.Unauthorized, "请登陆后再访问", string.Empty);
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, data);
                return;
            }
            var userInfoWrapper = (UserInfoWrapper)actionContext.Request.GetDependencyScope().GetService(typeof(UserInfoWrapper));
            userInfoWrapper.JwtData= JwtHelper.Decode<JwtData>(token);
            if(userInfoWrapper.JwtData.ValidateTimeout())
            {
                var data = ResultDataWrapper.Create(System.Net.HttpStatusCode.Unauthorized, "登陆信息已过期", string.Empty);
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, data);
                return;
            }
            userInfoWrapper.UserInfo = UserInfo.lstUserInfo.First(x => x.Id == userInfoWrapper.JwtData.Id);
        }
    }
}