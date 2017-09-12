using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using Models;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Implements.Repositories
{
    public class CityRepository: IDbRepository<City>
    {
        private readonly CurrencyDatabaseEntities _db;
        public CityRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task<int> CountAsync => _db.City.CountAsync();

        public async Task<City> FindOrCreateAsync(City city)
        {
            var model = await _db.City.FirstOrDefaultAsync(c => c.Name == city.Name);
            if (model != null)
            {
                return model;
            }

            _db.City.Add(city);
            await _db.SaveChangesAsync();
            return await _db.City.FindAsync(city.CityId);
        }

        public async Task<IEnumerable<City>> GetAllAsync() => await _db.City.ToListAsync();

        public Task<City> GetByIdAsync(Guid id) => _db.City.FindAsync(id);
    }
}
