using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TylData.Data;

namespace TYLGallery
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();

            //The line below tells autofac, when a controller is initialized, pass into its constructor, the implementations of the required interfaces
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<TYLGalleryContext>().As<TYLGalleryContext>();
            builder.RegisterType<TylGalleryRepository>().As<ITylGalleryRepository>().InstancePerLifetimeScope();


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var myContainer = builder.Build();
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(myContainer);

            // MVC - Set the dependency resolver to be Autofac.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(myContainer));


        }
    }
}
