using Abstracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataStructs;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace Implements
{
    public class ParserPresenter : IParserPresenter
    {
        private const int TasksAmount = 5;
        private readonly List<string> _urls = new List<string>
        {
            "https://myfin.by/currency/minsk"
        };

        private readonly ILoader _loader;
        private readonly IParserLoader _parserLoader;
        private readonly ICityParser _cityParser;
        private readonly object _parseUrlSyncKey;
        private readonly object _parseSyncKey;


        public ParserPresenter(ILoader loader, IParserLoader parserLoader, ICityParser cityParser)
        {
            _loader = loader;
            _parserLoader = parserLoader;
            _cityParser = cityParser;
            _parseUrlSyncKey = new object();
            _parseSyncKey = new object();
    }

        public async Task<IEnumerable<CurrencyRate>> ParseAsync()
        {
            var tasks = new List<Task<IEnumerable<CurrencyRate>>>();
            var result = new List<CurrencyRate>();
            foreach (var url in _urls)
            {
                var nodes = await ParseUrlAsync(url);
                result.AddRange(nodes);
            }
            return result;
        }

        private async Task<IEnumerable<CurrencyRate>> ParseUrlAsync(string url)
        {
            var html = await _loader.LoadAsync(url);
            var parser = _parserLoader.GetParser(url);
            if (parser == null)
            {
                return new List<CurrencyRate>();
            }

            var citiesUrls = _cityParser.Parse(html);
            var result = new List<CurrencyRate>();
            var allTasks = new List<Task>();
            var throttler = new SemaphoreSlim(TasksAmount);
            
            foreach (var cityUrl in citiesUrls)
            {
                await throttler.WaitAsync();
                allTasks.Add(
                    Task.Run(async () =>
                    {
                        try
                        {
                            html = await _loader.LoadAsync(cityUrl);
                            var nodes = parser.Parse(html);
                            lock (_parseUrlSyncKey)
                            {
                                result.AddRange(nodes);
                                foreach (var item in nodes)
                                {
                                    Debug.WriteLine(item);
                                }
                            }
                        }
                        finally
                        {
                            throttler.Release();
                        }
                    }));
            }
            await Task.WhenAll(allTasks);
            return result;
        }
    }
}
