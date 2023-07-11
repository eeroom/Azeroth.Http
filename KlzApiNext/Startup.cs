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

namespace KlzApiNext
{
    public class Startup : Microsoft.AspNetCore.Hosting.IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use((Microsoft.AspNetCore.Http.HttpContext context,Func<Task> fac)=>{
                if (context.Request.Method.ToLower() != "OPTIONS".ToLower())
                {
                    var headerOrigin = context.Request.Headers["Origin"];
                    context.Response.Headers.Add("Access-Control-Allow-Origin", headerOrigin);
                    //允许浏览器端js读取响应的cookie
                    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                    //允许浏览器端js读取响应头Authorization的值
                    context.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
                    return fac();
                }
                else
                {
                    var headerOrigin = context.Request.Headers["Origin"];
                    context.Response.Headers.Add("Access-Control-Allow-Origin", headerOrigin);
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT");
                    //这个allow header，高版本火狐有x-request-header
                    //context.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "content-type");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", context.Request.Headers["Access-Control-Request-Headers"]);
                    context.Response.Headers.Add("Access-Control-Max-Age", "1728000");
                    return Task.FromResult(string.Empty);
                }

                
            });
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
