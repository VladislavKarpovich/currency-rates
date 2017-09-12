using Abstracts;
using Abstracts.Repositories;
using Abstracts.Services;
using Implements;
using Implements.Repositories;
using Implements.Services;
using Models;
using MyfinParserImplement;
using Ninject;
using System;
using System.Threading.Tasks;

namespace ConsoleAppForParsingTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ICityParser>().To<CityParser>();
                kernel.Bind<ICurrencyRateParser>().To<MyfinParser>();
                kernel.Bind<ILoader>().To<Loader>();
                kernel.Bind<IParserLoader>().To<ParserLoader>();
                kernel.Bind<IParserPresenter>().To<ParserPresenter>();

                kernel.Bind<IDbRepository<Bank>>().To<BankRepository>();
                kernel.Bind<IDbRepository<City>>().To<CityRepository>();
                kernel.Bind<IDbRepository<Currency>>().To<CurrencyRepository>();
                kernel.Bind<IDbRepository<Office>>().To<OfficeRepository>();
                kernel.Bind<ICrossRateRepository>().To<CrossRateRepository>();
                kernel.Bind<CurrencyDatabaseEntities>().ToSelf().InTransientScope();

                kernel.Bind<IBankService>().To<BankService>();
                kernel.Bind<IParserProcess>().To<ParserProcess>();
                var parsing = kernel.Get<IParserProcess>();

                AsyncJob(parsing).Wait();
                Console.WriteLine("the end");
                Console.ReadKey();
            }
        }

        private static async Task AsyncJob(IParserProcess parser)
        {
            try
            {
                await parser.RunAsync();
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
