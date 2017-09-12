using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Implements.Repositories
{
    public class OfficeRepository : IDbRepository<Office>
    {
        private readonly CurrencyDatabaseEntities _db;
        public OfficeRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task<int> CountAsync => _db.Office.CountAsync();

        public async Task<Office> FindOrCreateAsync(Office item)
        {
            var model = await _db.Office.FirstOrDefaultAsync(c => c.Tittle == item.Tittle);
            if (model != null)
            {
                return model;
            }

            _db.Office.Add(item);
            await _db.SaveChangesAsync();
            return await _db.Office.FindAsync(item.OfficeId);
        }

        public async Task<IEnumerable<Office>> GetAllAsync() => await _db.Office.ToListAsync();

        public Task<Office> GetByIdAsync(Guid id) => _db.Office.FindAsync(id);
    }
}
