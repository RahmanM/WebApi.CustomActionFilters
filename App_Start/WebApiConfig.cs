using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.API.Filters.Filters.ActionFilters;

namespace Web.API.Filters
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new NullArgumentValidationFilter());
            config.Filters.Add(new ModelStateValidationFilter());
            config.Filters.Add(new PerformanceLoggerFilter());
            config.Filters.Add(new CustomAuthorizationFilterAttribute());
            //config.Filters.Add(new RequiredHttpsFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
