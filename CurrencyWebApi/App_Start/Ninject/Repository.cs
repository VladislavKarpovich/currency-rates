using Abstracts.Repositories;
using Implements.Repositories;
using Implements.Repositories.Cache;
using Models;
using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class Repository : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Bank>>().To<BankCacheRepository>().InSingletonScope();
            Bind<IRepository<City>>().To<CityCacheRepository>().InSingletonScope();
            Bind<IRepository<Currency>>().To<CurrencyCacheRepository>().InSingletonScope();
            Bind<IRepository<Office>>().To<OfficeCacheRepository>().InSingletonScope();

            Bind<ICrossRateRepository<CrossRate>>().To<CrossRateRepository>();
            Bind<ICrossRateRepository<ActualCrossRate>>().To<ActualCrossRateRepository>();

            Bind<IRepository<Bank>>().To<BankRepository>().WhenInjectedInto<BankCacheRepository>();
            Bind<IRepository<City>>().To<CityRepository>().WhenInjectedInto<CityCacheRepository>();
            Bind<IRepository<Currency>>().To<CurrencyRepository>().WhenInjectedInto<CurrencyCacheRepository>();
            Bind<IRepository<Office>>().To<OfficeRepository>().WhenInjectedInto<OfficeCacheRepository>();

            Bind<CurrencyDatabaseEntities>().ToSelf().InTransientScope();
        }
    }
}