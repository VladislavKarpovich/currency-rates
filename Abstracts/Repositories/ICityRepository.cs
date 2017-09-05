using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);
        IEnumerable<City> GetAllCities();
        Task<City> GetCityAsync(Guid id);
    }
}
