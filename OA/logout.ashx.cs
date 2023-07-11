using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    /// <summary>
    /// logout 的摘要说明
    /// </summary>
    public class logout : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
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