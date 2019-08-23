using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Owin;
using Nancy.Owin;
[assembly: Microsoft.Owin.OwinStartup(typeof(Azeroth.WebApiNancy.Startup))]
namespace Azeroth.WebApiNancy
{
    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}