using Abstracts.Repositories;
using CacheManager.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implements.Repositories.Cache
{
    public class BankCacheRepository : IRepository<Bank>
    {
        private readonly ICacheManager<Bank> _cache;
        private readonly IRepository<Bank> _repository;

        public BankCacheRepository(IRepository<Bank> repository)
        {
            _repository = repository;
            _cache = CacheFactory.Build<Bank>("BankCache", settings => 
            {
                settings.WithSystemRuntimeCacheHandle("handleBank");
            });
        }

        public async Task<Bank> CreateAsync(Bank item)
        {
            var newModel = await _repository.FindOrCreateAsync(item);
            _cache.Add(item.Name, newModel);
            return newModel;
        }

        public Task<Bank> FindAsync(Bank item)
        {
            var tcs = new TaskCompletionSource<Bank>();
            tcs.SetResult(_cache.Get(item.Name));
            return tcs.Task;
        }

        public Task<IEnumerable<Bank>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Bank> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
    }
}
