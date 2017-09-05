using DataStructs;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Abstracts
{
    public interface ICurrencyRateParser
    {
        IEnumerable<CurrencyRate> Parse(HtmlDocument htmlDoc);
    }
}
