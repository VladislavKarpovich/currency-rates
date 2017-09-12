using System.Web.Http;
using Ninject;
using Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Modules;
using CurrencyWebApi.App_Start.Ninject;

namespace ExadelPractice
{
    public static class NinjectConfig
    {
        public static void Register(IAppBuilder app, HttpConfiguration config, IKernel kernel)
        {
            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new INinjectModule[]
            {
                new Repository(),
                new Service(),
                new Parser(),
                new BackgroundScope()
            });

            return kernel;
        }
    }
}