using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface IBankRepository
    {
        Task AddAsync(Bank bank);
        IEnumerable<Bank> GetAll();
        Task<Bank> GetByIdAsync(Guid id);
    }
}
