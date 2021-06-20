using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRIIS
{
    public partial class ChatRoom : MyPage
    {
        public override void ProcessRequest(HttpContext context)
        {
            if (context.Session["userInfo"] == null)
                context.Response.Redirect("/Login.aspx");
            base.ProcessRequest(context);
        }
        protected override void OnLoad(EventArgs e)
        {
            string op = this.Request["op"];
            if (op != "msg")
                return;
            string msg = this.Request["msg"];
            var hubcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<LolHub>();
            var all = hubcontext.Clients.All;
            all.refresh(this.Session["userInfo"] as string, msg);
            this.Response.End();
        }
    }
}