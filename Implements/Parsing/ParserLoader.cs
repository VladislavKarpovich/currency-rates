using Abstracts;
using MyfinParserImplement;
using System.Collections.Generic;

namespace Implements
{
    public class ParserLoader : IParserLoader
    {
        private readonly Dictionary<string, ICurrencyRateParser> _parsers = new Dictionary<string, ICurrencyRateParser>
        {
            {"https://myfin.by/currency/minsk", new MyfinParser()}
        };

        public ICurrencyRateParser GetParser(string url)
        {
            try
            {
                return _parsers[url];
            }
            catch (KeyNotFoundException)
            {

                return null;
            }
        }
    }
}
