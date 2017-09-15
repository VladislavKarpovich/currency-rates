using Hangfire;
using ExadelPractice;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject;
using CurrencyWebApi.Backgrounds;

[assembly: OwinStartup(typeof(CurrencyWebApi.Startup))]

namespace CurrencyWebApi
{
    public class Startup
    {
        const string conncectionString = @"data source=(localdb)\MSSQLLocalDB;initial catalog=CurrencyDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var kernel = NinjectConfig.CreateKernel();

            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(conncectionString);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            NinjectConfig.Register(app, config, kernel);
            WebApiConfig.Register(config);

            RunBackgroundTasks(kernel);
        }

        private void RunBackgroundTasks(IKernel kernel)
        {
            var backgroundParsing = kernel.Get<ParsingBackground>();
            backgroundParsing.Run();
        }
    }
}
