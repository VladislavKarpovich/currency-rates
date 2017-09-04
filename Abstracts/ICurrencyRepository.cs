using Models;
using System.Collections.Generic;
using System.Linq;

namespace Abstracts
{
    public interface ICurrencyRepository
    {
        void AddRange(IEnumerable<CurrencyRate> rates);
        IQueryable<CurrencyRate> GetRates();
    }
}
