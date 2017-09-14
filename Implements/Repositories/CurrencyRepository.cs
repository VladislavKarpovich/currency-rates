using Abstracts.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    public class CurrencyRepository : IRepository<Currency>
    {

        private readonly CurrencyDatabaseEntities _db;
        public CurrencyRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task<int> CountAsync => _db.Currency.CountAsync();

        public async Task<Currency> CreateAsync(Currency item)
        {
            _db.Currency.Add(item);
            await _db.SaveChangesAsync();
            return await _db.Currency.FindAsync(item.CurrencyId);
        }

        public Task<Currency> FindAsync(Currency cur) => _db.Currency.FirstOrDefaultAsync(c => c.Name == cur.Name);

        public async Task<IEnumerable<Currency>> GetAllAsync() => await _db.Currency.ToListAsync();

        public Task<Currency> GetByIdAsync(Guid id) => _db.Currency.FindAsync(id);
    }
}
