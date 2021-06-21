using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRIIS
{
    public partial class Login : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            string op = this.Request["op"];
            if (op != "submit")
                return;
            string userName = this.Request["UserName"];
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(userName, true);
        }
    }
}