using Abstracts.Services;
using Hangfire;
using System.Diagnostics;

namespace CurrencyWebApi.Backgrounds
{
    public class ParsingBackground
    {
        public ParsingBackground(IParserProcess parserProcess)
        {
            RecurringJob.AddOrUpdate(() => parserProcess.RunAsync(), Cron.Hourly);
        }
    }
}