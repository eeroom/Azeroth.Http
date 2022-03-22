using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KlzSignalR
{
    public class LolHub : Microsoft.AspNet.SignalR.Hub
    {
        public static Dictionary<string, string> DictUserConnectionId = new Dictionary<string, string>();
        public override Task OnConnected() {

            if (DictUserConnectionId.ContainsKey(this.Context.User.Identity.Name))
                DictUserConnectionId[this.Context.User.Identity.Name] = this.Context.ConnectionId;
            else
                DictUserConnectionId.Add(this.Context.User.Identity.Name, this.Context.ConnectionId);
            return base.OnConnected();
        }
    }
}