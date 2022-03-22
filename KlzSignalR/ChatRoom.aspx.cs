using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KlzSignalR
{
    public partial class ChatRoom : CmdPage<ChatRoom>
    {
        public void SendMessage(HttpContext context) {
            string msg = context.Request["msg"];
            var hubcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<LolHub>();
            hubcontext.Clients.All.refresh(context.User.Identity.Name, msg);
            if (LolHub.DictUserConnectionId.ContainsKey(context.User.Identity.Name)) {
                hubcontext.Clients.Client(LolHub.DictUserConnectionId[context.User.Identity.Name]).refresh("我自己", msg);
            }
          
        }
    }
}