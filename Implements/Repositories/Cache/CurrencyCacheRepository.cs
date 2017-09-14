using Abstracts.Repositories;
using CacheManager.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implements.Repositories.Cache
{
    public class CurrencyCacheRepository : IRepository<Currency>
    {
        private readonly ICacheManager<Currency> _cache;
        private readonly IRepository<Currency> _repository;

        public CurrencyCacheRepository(IRepository<Currency> repository)
        {
            _repository = repository;
            _cache = CacheFactory.Build<Currency>("CurrencyCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("handleCurrency");
            });
        }

        public async Task<Currency> CreateAsync(Currency item)
        {
            var newModel = await _repository.FindOrCreateAsync(item);
            _cache.Add(item.Name, newModel);
            return newModel;
        }

        public Task<Currency> FindAsync(Currency item)
        {
            var tcs = new TaskCompletionSource<Currency>();
            tcs.SetResult(_cache.Get(item.Name));
            return tcs.Task;
        }

        public Task<IEnumerable<Currency>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Currency> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

    }
}
