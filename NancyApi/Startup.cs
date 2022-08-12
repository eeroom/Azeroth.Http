using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Owin;
using Nancy.Owin;
[assembly: Microsoft.Owin.OwinStartup(typeof(NancyApi.Startup))]
namespace NancyApi
{
    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}