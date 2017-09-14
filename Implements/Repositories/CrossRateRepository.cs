using Abstracts.Repositories;
using System.Threading.Tasks;
using Models;
using System.Linq;
using System.Data.Entity;

namespace Implements.Repositories
{
    public class CrossRateRepository: ICrossRateRepository<CrossRate>
    {
        private readonly CurrencyDatabaseEntities _db;
        public CrossRateRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task AddCrossRateAsync(CrossRate crossRate)
        {
            _db.CrossRate.Add(crossRate);
            return _db.SaveChangesAsync();
        }

        public Task CleanTable()
        {
            return _db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [CrossRate]");
        }
    }
}
