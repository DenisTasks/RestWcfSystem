﻿using System;
using System.Web;
using System.Web.Http;
using BLL.BLLService;
using BLL.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Model;
using Model.Interfaces;
using Model.ModelService;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi;
using WebApiNET;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WebApiNET
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
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
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            kernel.Bind<WPFOutlookContext>().ToSelf().InScope(s => HttpContext.Current).WithConstructorArgument("connectionString", "WPFOutlookContext");
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IBLLServiceMain>().To<BLLServiceMain>();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<INotifyService>().To<NotifyService>();
        }
    }
}