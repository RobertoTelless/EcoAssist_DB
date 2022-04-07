using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using ModelServices.Interfaces.Repositories;
using ApplicationServices.Services;
using ModelServices.EntitiesServices;
using DataServices.Repositories;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Presentation.Start.NinjectWebCommons), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Presentation.Start.NinjectWebCommons), "Stop")]

namespace Presentation.Start
{
    public class NinjectWebCommons
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

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<ICRAppService>().To<CRAppService>();
            kernel.Bind<ICPAppService>().To<CPAppService>();
            kernel.Bind<ILPAppService>().To<LPAppService>();
            kernel.Bind<IPCAppService>().To<PCAppService>();
            kernel.Bind<IEPAppService>().To<EPAppService>();
            kernel.Bind<IENAppService>().To<ENAppService>();
            kernel.Bind<IOSEspAppService>().To<OSEspAppService>();
            kernel.Bind<IOSSitAppService>().To<OSSitAppService>();
            kernel.Bind<IOrdemServicoAppService>().To<OrdemServicoAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<ICRService>().To<CRService>();
            kernel.Bind<ICPService>().To<CPService>();
            kernel.Bind<ILPService>().To<LPService>();
            kernel.Bind<IPCService>().To<PCService>();
            kernel.Bind<IEPService>().To<EPService>();
            kernel.Bind<IENService>().To<ENService>();
            kernel.Bind<IOSEspService>().To<OSEspService>();
            kernel.Bind<IOSSitService>().To<OSSitService>();
            kernel.Bind<IOrdemServicoService>().To<OrdemServicoService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<ICRRepository>().To<CRRepository>();
            kernel.Bind<ICPRepository>().To<CPRepository>();
            kernel.Bind<ILPRepository>().To<LPRepository>();
            kernel.Bind<IPCRepository>().To<PCRepository>();
            kernel.Bind<IEPRepository>().To<EPRepository>();
            kernel.Bind<IENRepository>().To<ENRepository>();
            kernel.Bind<IOSEspRepository>().To<OSEspRepository>();
            kernel.Bind<IOSSitRepository>().To<OSSitRepository>();
            kernel.Bind<IOrdemServicoRepository>().To<OrdemServicoRepository>();

        }
    }
}