using Moq;
using Odh.BooksDemo.Domain.Abstract;
using Odh.BooksDemo.Domain.Concrete;
using Odh.BooksDemo.Web.Infrastructure.Abstract;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Odh.BooksDemo.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Odh.BooksDemo.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Odh.BooksDemo.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IBooksDemoUow>().To<BooksDemoUow>();
            //provide a mock user 
            var mockSession = new Mock<ISessionHandler>();
            mockSession.Setup(m => m.UserId).Returns(1025);
            mockSession.Setup(m => m.UserName).Returns("Test User");
            mockSession.Setup(m => m.SsoUserToken).Returns("2dccaa08-525f-46db-ad7e-ff3959db68d0");
            kernel.Bind<ISessionHandler>().ToConstant(mockSession.Object);
            //bind to concrete session once you set up users and roles
            //kernel.Bind<ISessionHandler>().To<SessionHandler>();

            //provide mock authentication.
            var auth = new Mock<IAuthProvider>();
            auth.Setup(m => m.SsoAuthenticate(It.IsAny<string>(), It.IsAny<String>())).Returns(AuthenticationStatus.Authenticated);
            kernel.Bind<IAuthProvider>().ToConstant(auth.Object);
            //bind to Gateway single sign on once you have set up your gateway application.
            //kernel.Bind<IAuthProvider>().To<SsoAuthProvider>();
        }
    }
}
