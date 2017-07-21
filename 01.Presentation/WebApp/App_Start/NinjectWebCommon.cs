[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace WebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using MongoDB.Driver;

    using CORE_REPO = Core.Repository;
    using BE = BusinessEntities;
    using CORE_REPO_MONGO = Core.Repository.MongoDb;
    using SVC = MongoDB.Service;

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
            kernel.Bind<IMongoClient>().To<MongoClient>();

            kernel.Bind<SVC.IBookService>().To<SVC.BookService>();
            kernel.Bind<CORE_REPO.IRepository<BE.Book>>().To<CORE_REPO_MONGO.MongoRepository<BE.Book>>();

            kernel.Bind<SVC.IUserService>().To<SVC.UserService>();
            kernel.Bind<CORE_REPO.IRepository<BE.User>>().To<CORE_REPO_MONGO.MongoRepository<BE.User>>();

            kernel.Bind<SVC.IDemandService>().To<SVC.DemandService>();
            kernel.Bind<CORE_REPO.IRepository<BE.Demand>>().To<CORE_REPO_MONGO.MongoRepository<BE.Demand>>();

            kernel.Bind<Models.IApiClient<BE.Book>>().To<Models.ApiClient<BE.Book>>();
        }        
    }
}
