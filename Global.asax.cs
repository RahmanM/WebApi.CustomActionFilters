using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace Web.API.Filters
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                AutofacDependencyResolverHelper.RegisterAutofac();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
        }

        public class AutofacDependencyResolverHelper
        {
            public static void RegisterAutofac()
            {
                var container = GetContainer();
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            }

            public static IContainer GetContainer()
            {
                var assemblies = new List<Assembly>();
                assemblies.Add(Assembly.Load("Web.API.Filters.DLL"));
               // assemblies.Add(Assembly.Load("Interfaces.DLL"));

                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(assemblies.ToArray()).AsImplementedInterfaces();

                // Note: injecting connectiong string if needed
                //builder.RegisterType(typeof(CrmContext)).As(typeof(DbContext)).
                //    WithParameter("<<ConnectionStringName>>>", @"Actual_Connection_String").InstancePerRequest();


                builder.RegisterControllers(Assembly.Load("Web.API.Filters")); // Name of the dll for MVC project
                var container = builder.Build();
                return container;
            }
        }

    }
}
