using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
namespace KlzApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Http.GlobalConfiguration.Configuration.Routes.MapHttpRoute("htt", "{controller}/{action}");
            System.Web.Http.GlobalConfiguration.Configuration.Filters.Add(new HaishanExceptionHandler());
            System.Web.Http.GlobalConfiguration.Configuration.Filters.Add(new HaishanActionFilter());

            var flagIScoped = typeof(IScoped);
            var flagITransient = typeof(ITransient);
            var lstAssembly= System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<System.Reflection.Assembly>().ToList();
            var lstType= lstAssembly.SelectMany(x => x.GetTypes()).ToList();
            var lstIScoped = lstType.Where(x => x != flagIScoped && flagIScoped.IsAssignableFrom(x)).ToArray();
            var lstITransient = lstType.Where(x => x != flagITransient && flagITransient.IsAssignableFrom(x)).ToArray();

            var builder= new Autofac.ContainerBuilder();
            builder.RegisterTypes(lstIScoped).AsSelf().AsImplementedInterfaces().PropertiesAutowired()
                .InstancePerApiRequest();
            builder.RegisterTypes(lstITransient).AsSelf().AsImplementedInterfaces().PropertiesAutowired()
                .InstancePerDependency();
            var controllerAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterApiControllers(controllerAssembly).AsSelf().PropertiesAutowired()
                .InstancePerDependency();
            var resolver= new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(builder.Build());
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }

    }
}