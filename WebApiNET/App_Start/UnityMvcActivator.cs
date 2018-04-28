using System.Linq;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApiNET.UnityMvcActivator), nameof(WebApiNET.UnityMvcActivator.Start))]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WebApiNET.UnityMvcActivator), nameof(WebApiNET.UnityMvcActivator.Shutdown))]

namespace WebApiNET
{
    public static class UnityMvcActivator
    {
        public static void Start()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}