using CraftBeer.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftBeer.Api.Domain
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}