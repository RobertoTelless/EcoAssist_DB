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
            kernel.Bind<IClienteAppService>().To<ClienteAppService>();
            kernel.Bind<IOrdemServicoAppService>().To<OrdemServicoAppService>();
            kernel.Bind<IPrestadorAppService>().To<PrestadorAppService>();
            kernel.Bind<IAtendimentoAppService>().To<AtendimentoAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IOrdemServicoService>().To<OrdemServicoService>();
            kernel.Bind<IClienteService>().To<ClienteService>();
            kernel.Bind<IPrestadorService>().To<PrestadorService>();
            kernel.Bind<IAtendimentoService>().To<AtendimentoService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IOrdemServicoRepository>().To<OrdemServicoRepository>();
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<IClienteEnderecoRepository>().To<ClienteEnderecoRepository>();
            kernel.Bind<IPrestadorRepository>().To<PrestadorRepository>();
            kernel.Bind<IAtendimentoRepository>().To<AtendimentoRepository>();
            kernel.Bind<ITipoClienteRepository>().To<TipoClienteRepository>();
            kernel.Bind<IOrigemClienteRepository>().To<OrigemClienteRepository>();
            kernel.Bind<IAtendimentoStatusRepository>().To<AtendimentoStatusRepository>();
            kernel.Bind<ICategoriaAtendimentoRepository>().To<CategoriaAtendimentoRepository>();
            kernel.Bind<IDepartamentoRepository>().To<DepartamentoRepository>();
            kernel.Bind<IParceiroRepository>().To<ParceiroRepository>();
            kernel.Bind<IStatusOrdemServicoRepository>().To<StatusOrdemServicoRepository>();
            kernel.Bind<ITipoOrdemServicoRepository>().To<TipoOrdemServicoRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();

        }
    }
}