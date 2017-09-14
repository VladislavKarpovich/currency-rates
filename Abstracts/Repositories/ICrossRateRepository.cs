using Models;
using System.Threading.Tasks;

namespace Abstracts.Repositories
{
    public interface ICrossRateRepository<T>
    {
        Task AddCrossRateAsync(T rate);
        Task CleanTable();
    }
}
