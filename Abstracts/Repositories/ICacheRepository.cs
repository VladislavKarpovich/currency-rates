using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    interface ICacheRepository<T> where T : class
    {
        Task<T> FindOrCreateAsync(T bank);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}
