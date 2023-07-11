using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wowonline
{
    public class CmdPage<T>:System.Web.UI.Page
    {
        static Type meta = typeof(T);
        public override void ProcessRequest(HttpContext context) {
            var tmppage = this as IAnonymousPage;
            if (tmppage == null)
                this.Authentication(context);
            var cmd = context.Request["cmd"];
            if (string.IsNullOrEmpty(cmd)) {
                base.ProcessRequest(context);
                return;
            }
            var method = meta.GetMethod(cmd, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase);
            var rt = method.Invoke(this, new object[] { context });
            context.Response.ContentType = "application/json";
            var rtjson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(rt);
            context.Response.Write(rtjson);
            context.Response.End();
        }

        private void Authentication(HttpContext context) {
            if (!context.User.Identity.IsAuthenticated) {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                return;
            }

            var fid = context.User.Identity as System.Web.Security.FormsIdentity;
            //优化滑动过期，默认过半才更新，这里只要超过5分钟就更新新的
            if (fid.Ticket.IssueDate.AddMinutes(5) < DateTime.Now) {
                System.Web.Security.FormsAuthentication.SetAuthCookie(context.User.Identity.Name, true);
            }
        }
    }
}