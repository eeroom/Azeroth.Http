﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpChannelClient {
    class Program {
        static void Main(string[] args) {
            var client=new System.Runtime.Remoting.Channels.Tcp.TcpClientChannel();
        }
    }
}
