using System.Web.Http;

using Unity.AspNet.WebApi;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApiNET.UnityWebApiActivator), nameof(WebApiNET.UnityWebApiActivator.Start))]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WebApiNET.UnityWebApiActivator), nameof(WebApiNET.UnityWebApiActivator.Shutdown))]

namespace WebApiNET
{
    public static class UnityWebApiActivator
    {
        public static void Start()
        {
            var resolver = new UnityDependencyResolver(UnityConfig.Container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}