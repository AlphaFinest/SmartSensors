using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Microsoft.AspNet.Identity.Owin;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.UrlProvider;
using SmartSensors.Service;
using SmartSensors.Service.Seeding;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.ViewModels;
using SmartSensors.Controllers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SmartSensors.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SmartSensors.App_Start.NinjectWebCommon), "Stop")]

namespace SmartSensors.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        public static IKernel Kernel
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            Kernel = new StandardKernel();
            try
            {
                Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(Kernel);
                return Kernel;
            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationUserManager>()
               .ToMethod(_ => HttpContext
               .Current
               .GetOwinContext()
               .GetUserManager<ApplicationUserManager>());

            kernel.Bind<ApplicationDbContext>()
                .ToMethod(_ => HttpContext
                .Current
                .GetOwinContext()
                .GetUserManager<ApplicationDbContext>())
                .InRequestScope();

            kernel.Bind<IUrlProvider>().To<UrlProvider>().WhenInjectedInto<UrlProviderDecorator>();

            kernel.Bind<IUrlProvider>().To<UrlProviderDecorator>();

            kernel.Bind<HttpContext>().ToMethod(_ => HttpContext.Current).WhenInjectedInto<UrlProviderDecorator>();

            kernel.Bind<IValueTypeProvider>().To<ValueTypeProvider>();

            kernel.Bind<ISensorService>().To<SensorService>();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<ISeeder>().To<RoleSeeder>().Named("roleSeeder");
            kernel.Bind<ISeeder>().To<AdminSeeder>().Named("adminSeeder");
            
        }        
    }
}
