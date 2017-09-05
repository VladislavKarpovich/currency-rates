using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using Models;
using System.Threading.Tasks;
using System.Linq;

namespace Implements.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly CurrencyDatabaseEntities _db;
        public CityRepository(CurrencyDatabaseEntities db)
        {
            _db = db;
        }

        public async Task AddAsync(City city)
        {
            _db.City.Add(city);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<City> GetAllCities() => _db.City.ToList();

        public async Task<City> GetCityAsync(Guid id) => await _db.City.FindAsync(id);
    }
}
