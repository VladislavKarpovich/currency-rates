using Abstracts.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    public class ActualCrossRateRepository : ICrossRateRepository<ActualCrossRate>
    {
        private readonly CurrencyDatabaseEntities _db;
        public ActualCrossRateRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task AddCrossRateAsync(ActualCrossRate rate)
        {
            _db.ActualCrossRate.Add(rate);
            return _db.SaveChangesAsync();
        }

        public Task CleanTable()
        {
            return _db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [ActualCrossRate]");
        }
    }
}
