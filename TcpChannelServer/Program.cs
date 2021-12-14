using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpChannelServer {
    class Program {
        static void Main(string[] args) {
            var server = new System.Runtime.Remoting.Channels.Tcp.TcpServerChannel(9009);
        }
    }
}
