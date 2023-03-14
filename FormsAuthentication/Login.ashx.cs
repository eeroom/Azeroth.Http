using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormsAuthentication
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userName= context.Request["userName"];
            if (string.IsNullOrEmpty(userName))
            {
                context.Response.Write("请输入用户名");
                return;
            }
            var pwd = context.Request["pwd"];
            if (string.IsNullOrEmpty(pwd))
            {
                context.Response.Write("请输入密码");
                return;
            }
            if (pwd != "123456")
            {
                context.Response.Write("用户名或密码错误");
                return;
            }
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(userName, true);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}