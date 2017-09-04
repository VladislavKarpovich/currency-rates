using Models;
using System.Collections.Generic;

namespace Abstracts
{
    public interface IParser
    {
        IEnumerable<CurrencyRate> Parse();
    }
}
