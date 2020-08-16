using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Hg.Client
{
    class AsmxClientProxy : System.Runtime.Remoting.Proxies.RealProxy
    {
        public override IMessage Invoke(IMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
