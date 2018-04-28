using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Unity.Lifetime;
using WebApiNET.Util;

namespace WebApiNET
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.AddODataQueryFilter();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PageApi",
                routeTemplate: "api/{controller}/{page}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new CustomFormatter());
        }
    }
}
