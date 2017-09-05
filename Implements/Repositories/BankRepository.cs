using Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Implements.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly CurrencyDatabaseEntities _db;
        public BankRepository(CurrencyDatabaseEntities db)
        {
            _db = db;
        }


        public async Task AddAsync(Bank bank)
        {
            _db.Bank.Add(bank);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Bank> GetAll() => _db.Bank.ToList();

        public async Task<Bank> GetByIdAsync(Guid id) => await _db.Bank.FindAsync(id);
    }
}
