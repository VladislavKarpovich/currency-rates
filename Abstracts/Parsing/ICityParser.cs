using HtmlAgilityPack;
using System.Collections.Generic;

namespace Abstracts
{
    public interface ICityParser
    {
        IEnumerable<string> Parse(HtmlDocument htmlDoc);
    }
}
