using Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructs;

namespace Implements
{
    class ParserPresenter : IParserPresenter
    {
        private readonly List<string> _urls = new List<string>
        {
            "https://myfin.by/currency/minsk"
        };

        private readonly ILoader _loader;
        private readonly IParserLoader _parserLoader;
        private readonly ICityParser _cityParser;
        private readonly object _syncKey = new object();

        public ParserPresenter(ILoader loader, IParserLoader parserLoader, ICityParser cityParser)
        {
            _loader = loader;
            _parserLoader = parserLoader;
            _cityParser = cityParser;
        }

        public IEnumerable<CurrencyRate> Parse()
        {
            var tasks = new List<Task<IEnumerable<CurrencyRate>>>();
            var result = new List<CurrencyRate>();
            Parallel.ForEach(_urls, (url) =>
            {
                var nodes = ParseUrl(url);
                result.Concat(nodes);
            });
            return result;
        }

        private IEnumerable<CurrencyRate> ParseUrl(string url)
        {
            var html = _loader.Load(url);
            var parser = _parserLoader.GetParser(url);
            if (parser == null)
            {
                return new List<CurrencyRate>();
            }

            var citiesUrls = _cityParser.Parse(html);
            var result = new List<CurrencyRate>();
            Parallel.ForEach(citiesUrls, (cityUrl) =>
            {
                html = _loader.Load(cityUrl);
                var nodes = parser.Parse(html);
                lock(_syncKey)
                {
                    result.Concat(nodes);
                }
            });

            return result;
        }
    }
}
