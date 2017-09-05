using DataStructs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts
{
    public interface IParserPresenter
    {
        IEnumerable<CurrencyRate> Parse();
    }
}
