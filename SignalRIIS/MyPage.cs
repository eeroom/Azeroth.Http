using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRIIS
{
    public class MyPage:System.Web.UI.Page
    {
        public override void ProcessRequest(HttpContext context) {
            if (!context.User.Identity.IsAuthenticated) {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                return;
            }
            var fid= context.User.Identity as System.Web.Security.FormsIdentity;
            //优化滑动过期，默认过半才更新，这里只要超过5分钟就更新新的
            if (fid.Ticket.IssueDate.AddMinutes(5) < DateTime.Now) {
                System.Web.Security.FormsAuthentication.SetAuthCookie(context.User.Identity.Name, true);
            }
            base.ProcessRequest(context);
        }

        protected override void OnInit(EventArgs e)
        {

            //base.OnInit(e);
        }
    }
}