[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BotecoPoker.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BotecoPoker.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace BotecoPoker.Mvc.App_Start
{
    using System;
    using System.Web;
    using BotecoPoker.Aplicacao.Servicos;
    using BotecoPoker.Aplicacao.Validadores;
    using BotecoPoker.Dominio.InterfacesRepositorio;
    using BotecoPoker.Infra.ClassesRepositorio;
    using BotecoPoker.Infra.Config;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

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
            kernel.Bind<IDbContexto>().To<ContextoRepositorio>().InRequestScope();
            kernel.Bind<IClienteRepositorio>().To<ClienteRepositorio>().InRequestScope();
            kernel.Bind<IItemVendaRepositorio>().To<ItemVendaRepositorio>().InRequestScope();
            kernel.Bind<IProdutoRepositorio>().To<ProdutoRepositorio>().InRequestScope();
            kernel.Bind<ITorneioClienteRepositorio>().To<TorneioClienteRepositorio>().InRequestScope();
            kernel.Bind<ITorneioRepositorio>().To<TorneioRepositorio>().InRequestScope();
            kernel.Bind<IUsuarioRepositorio>().To<UsuarioRepositorio>().InRequestScope();
            kernel.Bind<IVendaRepositorio>().To<VendaRepositorio>().InRequestScope();
            kernel.Bind<IPreVendaRepositorio>().To<PreVendaRepositorio>().InRequestScope();
            kernel.Bind<ICashGameRepositorio>().To<CashGameRepositorio>().InRequestScope();
            kernel.Bind<ICaixaRepositorio>().To<CaixaRepositorio>().InRequestScope();
            kernel.Bind<IPagamentoRepositorio>().To<PagamentoRepositorio>().InRequestScope();
            kernel.Bind<IParcelamentoPagamentoRepositorio>().To<ParcelamentoPagamentoRepositorio>().InRequestScope();
            kernel.Bind<ClienteAplicacao>().To<ClienteAplicacao>().InRequestScope();
            kernel.Bind<AutenticacaoAplicacao>().To<AutenticacaoAplicacao>().InRequestScope();
            kernel.Bind<LoginAplicacao>().To<LoginAplicacao>().InRequestScope();
            kernel.Bind<VendaAplicacao>().To<VendaAplicacao>().InRequestScope();
            kernel.Bind<ProdutoAplicacao>().To<ProdutoAplicacao>().InRequestScope();
            kernel.Bind<TorneioAplicacao>().To<TorneioAplicacao>().InRequestScope();
            kernel.Bind<TorneioClienteAplicacao>().To<TorneioClienteAplicacao>().InRequestScope();
            kernel.Bind<ValidadorCliente>().To<ValidadorCliente>().InRequestScope();
            kernel.Bind<ValidadorProduto>().To<ValidadorProduto>().InRequestScope();
            kernel.Bind<ValidadorTorneio>().To<ValidadorTorneio>().InRequestScope();
            kernel.Bind<ValidadorTorneioCliente>().To<ValidadorTorneioCliente>().InRequestScope();
            kernel.Bind<CashGameAplicacao>().To<CashGameAplicacao>().InRequestScope();
            kernel.Bind<DbContexto>().To<DbContexto>().InRequestScope();
        }        
    }
}
