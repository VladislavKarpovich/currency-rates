using Models;
using System.Collections.Generic;

namespace Abstracts.Services
{
    public interface IBankService
    {
        Bank FindOrCreate(Bank bank);
        IEnumerable<Bank> GetAll();
    }
}
