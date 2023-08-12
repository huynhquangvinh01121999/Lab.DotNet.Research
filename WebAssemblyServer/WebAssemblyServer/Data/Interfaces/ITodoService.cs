using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Data.Interfaces
{
    public interface ITodoService
    {
        Task<TodosList> CreateTodoAsync(TodosList todo);
        Task<bool> DeleteTodoAsync(int id);
        Task<TodosList> GetByIdAsync(int id);
        Task<IEnumerable<TodosList>> GetTodosAsync();
        Task<TodosList> UpdateTodoAsync(TodosList todo);
    }
}
