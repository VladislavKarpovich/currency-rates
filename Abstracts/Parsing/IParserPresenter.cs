using DataStructs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts
{
    public interface IParserPresenter
    {
        Task<IEnumerable<CurrencyRate>> ParseAsync();
    }
}
