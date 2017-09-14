using Abstracts;
using Abstracts.Repositories;
using Abstracts.Services;
using Implements;
using Implements.Repositories;
using Implements.Repositories.Cache;
using Implements.Services;
using Models;
using MyfinParserImplement;
using Ninject;
using System;
using System.Threading;
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

                kernel.Bind<IRepository<Bank>>().To<BankCacheRepository>().InSingletonScope();
                kernel.Bind<IRepository<City>>().To<CityCacheRepository>().InSingletonScope();
                kernel.Bind<IRepository<Currency>>().To<CurrencyCacheRepository>().InSingletonScope();
                kernel.Bind<IRepository<Office>>().To<OfficeCacheRepository>().InSingletonScope();

                kernel.Bind<ICrossRateRepository<CrossRate>>().To<CrossRateRepository>();
                kernel.Bind<ICrossRateRepository<ActualCrossRate>>().To<ActualCrossRateRepository>();

                kernel.Bind<IRepository<Bank>>().To<BankRepository>().WhenInjectedInto<BankCacheRepository>();
                kernel.Bind<IRepository<City>>().To<CityRepository>().WhenInjectedInto<CityCacheRepository>();
                kernel.Bind<IRepository<Currency>>().To<CurrencyRepository>().WhenInjectedInto<CurrencyCacheRepository>();
                kernel.Bind<IRepository<Office>>().To<OfficeRepository>().WhenInjectedInto<OfficeCacheRepository>();

                kernel.Bind<CurrencyDatabaseEntities>().ToSelf().InTransientScope();

                kernel.Bind<IBankService>().To<BankService>();
                kernel.Bind<IParserProcess>().To<ParserProcess>();
                var parsing = kernel.Get<IParserProcess>();

                for (int i = 0; i < 10; i++)
                {
                    AsyncJob(parsing).Wait();
                    Console.WriteLine(i);
                    Thread.Sleep(5000);
                }
                
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
