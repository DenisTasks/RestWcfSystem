[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebWCFHost.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebWCFHost.App_Start.NinjectWebCommon), "Stop")]

namespace WebWCFHost.App_Start
{
    using BLL.BLLService;
    using BLL.Interfaces;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Model;
    using Model.Interfaces;
    using Model.ModelService;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<WPFOutlookContext>().ToSelf().InRequestScope().WithConstructorArgument("connectionString", "WPFOutlookContext");
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>)).InRequestScope();
            kernel.Bind<IBllServiceMain>().To<BllServiceMain>().InRequestScope();
        }
    }
}