using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface IDbRepository<T> where T : class
    {
        Task<int> CountAsync { get; }
        Task<T> FindOrCreateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}
