using Abstracts.Repositories;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    internal static class RepositoryExtensions
    {
        public static async Task<T> FindOrCreateAsync<T>(this IRepository<T> repository, T item)
        {
            var model = await repository.FindAsync(item);
            return (model != null) ? model : await repository.CreateAsync(item);
        }
    }
}
