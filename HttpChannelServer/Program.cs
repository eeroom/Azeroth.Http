using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpChannelServer {
    class Program {
        static void Main(string[] args) {
            var server= new System.Runtime.Remoting.Channels.Http.HttpServerChannel();
        }
    }
}
