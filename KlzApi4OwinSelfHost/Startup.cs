using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KlzApi4OwinSelfHost
{
    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("htt", "{controller}/{action}");
            app.UseWebApi(config);
        }
    }
}
