using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.BlazorWasm.Domain.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<int> SaveChangeAsync();
    }
}
