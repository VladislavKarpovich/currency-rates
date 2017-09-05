using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface ICrossRateRepository
    {
        IEnumerable<CrossRate> GetAll();
        Task AddCrossRateAsync(CrossRate crossRate);
        Task<CrossRate> GetById(Guid id);
    }
}
