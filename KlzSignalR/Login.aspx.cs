using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KlzSignalR
{
    public partial class Login : CmdPage<Login>,IAnonymousPage
    {
        public void In(HttpContext context) {
            string userName = context.Request["UserName"];
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(userName, true);
        }
    }
}