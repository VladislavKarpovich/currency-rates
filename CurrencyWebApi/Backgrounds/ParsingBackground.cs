using Abstracts.Services;
using Hangfire;

namespace CurrencyWebApi.Backgrounds
{
    public class ParsingBackground
    {
        private readonly IParserProcess _parserProcess;
        public ParsingBackground(IParserProcess parserProcess)
        {
            _parserProcess = parserProcess;
            
        }

        public void Run()
        {
            RecurringJob.AddOrUpdate(() => _parserProcess.RunAsync(), Cron.Hourly);
        }
    }
}