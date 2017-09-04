using ExadelPractice;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(CurrencyWebApi.Startup))]

namespace CurrencyWebApi
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var kernel = NinjectConfig.CreateKernel();

            NinjectConfig.Register(app, config, kernel);
            WebApiConfig.Register(config);
        }
    }
}
