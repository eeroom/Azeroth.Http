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
        protected override void OnLoad(EventArgs e)
        {
            string op = this.Request["op"];
            if (op != "msg")
                return;
            string msg = this.Request["msg"];
            var hubcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<LolHub>();
            hubcontext.Clients.All.refresh(this.User.Identity.Name, msg);
            hubcontext.Clients.Client(LolHub.DictUserConnectionId[this.User.Identity.Name]).refresh("我自己", msg);
            this.Response.End();
        }
    }
}