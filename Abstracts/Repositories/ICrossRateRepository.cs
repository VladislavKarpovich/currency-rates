using Models;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface ICrossRateRepository
    {
        Task AddCrossRateAsync(CrossRate rate);
    }
}
