using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OA
{
    public partial class login : System.Web.UI.Page
    {
        public string UserName { get; set; }

        public string Pwd { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            this.UserName = this.Request["UserName"];
            this.Pwd = this.Request["Pwd"];
            if (string.IsNullOrEmpty(this.Pwd))
            {
                this.Pwd = "123456";
                return;
            }
            if (string.IsNullOrEmpty(this.UserName))
                return;
            if (this.Pwd != "123456")
                return;
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(this.UserName, true);
        }
    }
}