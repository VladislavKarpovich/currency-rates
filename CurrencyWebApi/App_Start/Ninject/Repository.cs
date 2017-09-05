using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class Repository : NinjectModule
    {
        public override void Load()
        {
            //Bind<IParser>().To<Parser>();
            //Bind<ICurrencyRepository>().To<CurrencyRepository>();
            //Bind<CurrencyWebApiContext>().ToSelf();
        }
    }
}