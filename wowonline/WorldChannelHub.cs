using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace wowonline
{
    public class WorldChannelHub : Microsoft.AspNet.SignalR.Hub
    {
       public static Dictionary<string, string> DictConnect = new Dictionary<string, string>();

        public override Task OnConnected() {

            if (DictConnect.ContainsKey(this.Context.User.Identity.Name))
                DictConnect[this.Context.User.Identity.Name] = this.Context.ConnectionId;
            else
                DictConnect.Add(this.Context.User.Identity.Name, this.Context.ConnectionId);
            return base.OnConnected();
        }

        public void SendMessageToAll(string msg)
        {
            this.Clients.Others.reciveMsg(this.Context.User.Identity.Name, msg);
            this.Clients.Caller.reciveMsg("我自己", msg);
        }
    }
}