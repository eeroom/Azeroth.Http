using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wowonline
{
    public partial class WorldChannel : CmdPage<WorldChannel>
    {
        public void SendMessage(HttpContext context) {
            string msg = context.Request["msg"];
            string target = context.Request["target"];
            //todo 把消息写入数据库
            //其他的业务逻辑

            //调用SignalR推送消息
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<WorldChannelHub>();
            var clients = hubContext.Clients;
            if (!WorldChannelHub.DictConnect.ContainsKey(target))
            {
                clients.Client(WorldChannelHub.DictConnect[context.User.Identity.Name]).reciveError(target + "未上线，消息发送失败");
                return;
            }
            clients.Client(WorldChannelHub.DictConnect[target]).reciveMsg(this.Context.User.Identity.Name, msg);
            clients.Client(WorldChannelHub.DictConnect[context.User.Identity.Name]).reciveMsg("我自己", msg);
        }
    }
}