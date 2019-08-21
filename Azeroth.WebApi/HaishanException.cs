using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.WebApi
{
    public class HaishanException:System.Exception
    {
        public HaishanException(string msg):base(msg)
        {

        }
    }
}