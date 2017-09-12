using Abstracts.Services;
using Abstracts.Repositories;
using System.Collections.Generic;
using Models;
using System.Threading.Tasks;

namespace Implements.Services
{
    public class BankService : IBankService
    {
        private readonly IDbRepository<Bank> _bankRepository;
        public BankService(IDbRepository<Bank> bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public Bank FindOrCreate(Bank bank) =>
            _bankRepository.FindOrCreateAsync(bank).Result;

        public IEnumerable<Bank> GetAll() => _bankRepository.GetAllAsync().Result;
    }
}
