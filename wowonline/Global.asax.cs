﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace wowonline
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Routing.SignalRRouteExtensions.MapHubs(System.Web.Routing.RouteTable.Routes);
        }

        
    }
}