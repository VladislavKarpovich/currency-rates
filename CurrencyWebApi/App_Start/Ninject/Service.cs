using Abstracts.Services;
using Implements.Services;
using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class Service: NinjectModule
    {
        public override void Load()
        {
            Bind<IBankService>().To<BankService>();
            Bind<IParserProcess>().To<ParserProcess>();
        }
    }
}