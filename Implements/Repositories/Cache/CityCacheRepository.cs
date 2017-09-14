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
    public class CityCacheRepository: IRepository<City>
    {
        private readonly ICacheManager<City> _cache;
        private readonly IRepository<City> _repository;
        public CityCacheRepository(IRepository<City> repository)
        {
            _repository = repository;
            _cache = CacheFactory.Build<City>("CityCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("handleCity");
            });
        }

        public Task<City> CreateAsync(City item)
        {
            var tcs = new TaskCompletionSource<City>();
            tcs.SetResult(_cache.Get(item.Name));
            return tcs.Task;
        }

        public async Task<City> FindAsync(City item)
        {
            var newModel = await _repository.FindOrCreateAsync(item);
            _cache.Add(item.Name, newModel);
            return newModel;
        }

        public Task<IEnumerable<City>> GetAllAsync() => _repository.GetAllAsync();

        public Task<City> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
    }
}
