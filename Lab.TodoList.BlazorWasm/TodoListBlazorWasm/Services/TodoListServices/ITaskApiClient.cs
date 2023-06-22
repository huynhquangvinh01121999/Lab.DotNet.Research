using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;

namespace TodoListBlazorWasm.Services.TodoListServices
{
    public interface ITaskApiClient
    {
        Task<IReadOnlyList<TaskViewModel>> GetTodosAsync();
        Task<TaskViewModel> GetTodoByIdAsync(int id);
        Task<TaskViewModel> CreateNewTaskAsync(CreateNewTaskRequest request);
        Task<bool> UpdateTaskAsync(UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(int id);
    }
}
