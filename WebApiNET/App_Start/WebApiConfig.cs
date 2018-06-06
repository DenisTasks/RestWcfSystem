using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.OData.Extensions;
using System.Web.Http.ValueProviders;
using System.Web.ModelBinding;
using BLL.EntitesDTO;
using WebApiNET.Util;
using IValueProvider = System.Web.Http.ValueProviders.IValueProvider;
using ModelBinderProvider = System.Web.Http.ModelBinding.ModelBinderProvider;
using SimpleModelBinderProvider = System.Web.Http.ModelBinding.Binders.SimpleModelBinderProvider;

namespace WebApiNET
{
    public class CustomValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new CustomValueProvider(actionContext);
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Add(typeof(ValueProviderFactory), new CustomValueProviderFactory());

            var provider = new SimpleModelBinderProvider(
                typeof(AppointmentDTO), new CustomModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            //config.MapHttpAttributeRoutes();
            config.AddODataQueryFilter();
            config.Formatters.Add(new CustomFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Route2",
                routeTemplate: "api2/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[] { new CustomHandlerWithController() })
            );

            config.Routes.MapHttpRoute(
                name: "Route3",
                routeTemplate: "api3/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new CustomHandlerWithoutController(config)
            );

            //config.MessageHandlers.Add(new CustomHandler3());


            //config.Routes.MapHttpRoute(
            //    name: "PageApi",
            //    routeTemplate: "api/{controller}/{page}",
            //    defaults: new { page = RouteParameter.Optional }
            //);
        }
    }
}
