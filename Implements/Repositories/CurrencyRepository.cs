using Abstracts.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    public class CurrencyRepository : IDbRepository<Currency>
    {

        private readonly CurrencyDatabaseEntities _db;
        public CurrencyRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task<int> CountAsync => _db.Currency.CountAsync();

        public async Task<Currency> FindOrCreateAsync(Currency currency)
        {
            var model = await _db.Currency.FirstOrDefaultAsync(c => c.Name == currency.Name);
            if (model != null)
            {
                return model;
            }

            _db.Currency.Add(currency);
            await _db.SaveChangesAsync();
            return await _db.Currency.FindAsync(currency.CurrencyId);
        }

        public async Task<IEnumerable<Currency>> GetAllAsync() => await _db.Currency.ToListAsync();

        public Task<Currency> GetByIdAsync(Guid id) => _db.Currency.FindAsync(id);
    }
}
