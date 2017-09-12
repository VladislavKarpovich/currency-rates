using Abstracts.Services;
using Hangfire;
using System.Diagnostics;

namespace CurrencyWebApi.Backgrounds
{
    public class ParsingBackground
    {
        private readonly IParserProcess _parserProcess;
        public ParsingBackground(IParserProcess parserProcess)
        {
            _parserProcess = parserProcess;

            //_parserProcess.Run();
            RecurringJob.AddOrUpdate(() => Debug.Write("Im working"), Cron.Minutely);
        }
    }
}