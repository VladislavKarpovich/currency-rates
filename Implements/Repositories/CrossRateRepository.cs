using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Implements.Repositories
{
    public class CrossRateRepository : ICrossRateRepository
    {
        private readonly CurrencyDatabaseEntities _db;
        public CrossRateRepository(CurrencyDatabaseEntities db)
        {
            _db = db;
        }

        public async Task AddCrossRateAsync(CrossRate crossRate)
        {
            _db.CrossRate.Add(crossRate);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<CrossRate> GetAll()
        {
            return _db.CrossRate.ToList();
        }

        public async Task<CrossRate> GetById(Guid id)
        {
            return await _db.CrossRate.FindAsync(id);
        }
    }
}
