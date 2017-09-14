using Abstracts.Repositories;
using CacheManager.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Repositories.Cache
{
    public class OfficeCacheRepository : IRepository<Office>
    {
        private readonly ICacheManager<Office> _cache;
        private readonly IRepository<Office> _repository;

        public OfficeCacheRepository(IRepository<Office> repository)
        {
            _repository = repository;
            _cache = CacheFactory.Build<Office>("OfficeCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("handleOffice");
            });
        }

        public async Task<Office> CreateAsync(Office item)
        {
            var newModel = await _repository.FindOrCreateAsync(item);
            _cache.Add(item.Tittle, newModel);
            return newModel;
        }

        public Task<Office> FindAsync(Office item)
        {
            var tcs = new TaskCompletionSource<Office>();
            tcs.SetResult(_cache.Get(item.Tittle));
            return tcs.Task;
        }

        public Task<IEnumerable<Office>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Office> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
    }
}
