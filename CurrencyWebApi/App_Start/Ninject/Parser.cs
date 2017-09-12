using Abstracts;
using Implements;
using MyfinParserImplement;
using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class Parser: NinjectModule
    {
        public override void Load()
        {
            Bind<ICityParser>().To<CityParser>();
            Bind<ICurrencyRateParser>().To<MyfinParser>();
            Bind<ILoader>().To<Loader>();
            Bind<IParserLoader>().To<ParserLoader>();
            Bind<IParserPresenter>().To<ParserPresenter>();
        }
    }
}