using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wowonline4aspnetcore
{
    public class WorldChannelHub : Microsoft.AspNet.SignalR.Hub {
        /// <summary>
        /// 这里定义的方法，客户端js可用通过js的WorldChannelHub的代理对象直接调用Send，代理对象内部做了真正的调用处理，又回到远程对象调用
        /// </summary>
        /// <param name="content"></param>
        public void Say(string content) {
            Console.WriteLine("服务器中转内容：" + content);
            //通过clients这个代理对象，可以直接调用客户端js定义好的refresh方法，代理对象内部做了真正的调用处理，又回到远程对象调用
            Clients.All.reciveMsg("服务器中转内容：" + content);
        }
    }
}
