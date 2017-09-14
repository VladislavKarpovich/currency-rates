using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T item);
        Task<T> FindAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}
