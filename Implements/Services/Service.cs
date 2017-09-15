using Abstracts.Services;
using Abstracts.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Implements.Services
{
    public class Service<T> : IService<T>
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<T> FindByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();
    }
}
