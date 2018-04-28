using BLL.BLLService;
using BLL.Interfaces;
using System;

using Unity;

namespace WebApiNET
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
            new Lazy<IUnityContainer>(() =>
            {
                var container = new UnityContainer();
                RegisterTypes(container);
                return container;
            });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<WPFOutlookContext, WPFOutlookContext>(new PerRequestLifetimeManager());
            container.RegisterType<IBLLServiceMain, BLLServiceMain>();
            //container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<INotifyService, NotifyService>();
        }
    }
}