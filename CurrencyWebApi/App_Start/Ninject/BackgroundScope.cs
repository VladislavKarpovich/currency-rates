using CurrencyWebApi.Backgrounds;
using Hangfire;
using Ninject.Modules;

namespace CurrencyWebApi.App_Start.Ninject
{
    public class BackgroundScope: NinjectModule
    {
        public override void Load()
        {
            Bind<ParsingBackground>().ToSelf().InBackgroundJobScope();
        }
    }
}