using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthWind.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // <-- ADD this overload to allow deleting by entity (useful for composite-key entities)
        Task DeleteAsync(T entity);
    }
}
