using System.Web.Http;
using System.Web.Http.OData.Extensions;
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
                defaults: new { page = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "RouteCreateResponse",
                routeTemplate: "api2/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new CustomHandler()
            );

            config.MessageHandlers.Add(new CustomHandler());

            config.Formatters.Add(new CustomFormatter());
        }
    }
}
