using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wowonline
{
    public partial class Login : CmdPage<Login>,IAnonymousPage
    {
        protected override void OnLoad(EventArgs e)
        {
            string userName = this.Request["UserName"];
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(userName, true);
        }
    }
}