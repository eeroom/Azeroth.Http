using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormsAuthentication
{
    /// <summary>
    /// Home 的摘要说明
    /// </summary>
    public class Home : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Home当前用户：" + context.User.Identity.Name);
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