using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace wowonline4aspnetcore
{
    public class WorldChannelController : ApiController {
        [HttpGet]
        public List<int> GetEntities() {
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }

        [HttpPost]
        public void Msg(ChatContent cc) {
            var hubcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<WorldChannelHub>();
            var all = hubcontext.Clients.All;
            all.reciveMsg("未知",cc.Msg);
        }
    }

    public class ChatContent {

        public string Msg { get; set; }
    }
}
