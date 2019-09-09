using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Azeroth.WebApiTZP
{
    public class Startup : Microsoft.AspNetCore.Hosting.IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(HandlerMvcRoute);
        }

        

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(HandlerMvcOptions);
            services.AddSingleton<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider,
                Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescriptionGroupCollectionProvider>();
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionProvider,
                Microsoft.AspNetCore.Mvc.ApiExplorer.DefaultApiDescriptionProvider>());
            return services.BuildServiceProvider();
        }

        private void HandlerMvcOptions(MvcOptions options)
        {
                
        }

        private void HandlerMvcRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("htt", "{controller}/{action}");
        }
    }
}
