using Abstracts.Repositories;
using Implements.Repositories;
using Models;
using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class Repository : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbRepository>().To<BankRepository>();
            Bind<CurrencyDatabaseEntities>().ToSelf();
        }
    }
}