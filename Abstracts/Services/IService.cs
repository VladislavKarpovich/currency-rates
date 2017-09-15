using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(Guid id);
    }
}
