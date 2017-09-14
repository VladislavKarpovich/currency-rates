using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Implements.Repositories
{
    public class BankRepository : IRepository<Bank>
    {
        private readonly CurrencyDatabaseEntities _db;
        public BankRepository(CurrencyDatabaseEntities db) => _db = db;

        public Task<int> CountAsync => _db.Bank.CountAsync();

        public async Task<Bank> CreateAsync(Bank bank)
        {
            _db.Bank.Add(bank);
            await _db.SaveChangesAsync();
            return await _db.Bank.FindAsync(bank.BankId);
        }

        public Task<Bank> FindAsync(Bank bank) => _db.Bank.FirstOrDefaultAsync(b => b.Name == bank.Name);

        public async Task<IEnumerable<Bank>> GetAllAsync() => await _db.Bank.ToListAsync();

        public Task<Bank> GetByIdAsync(Guid id) =>  _db.Bank.FindAsync(id);
    }
}
