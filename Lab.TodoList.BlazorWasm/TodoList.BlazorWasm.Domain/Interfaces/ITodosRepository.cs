using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces.Base;

namespace TodoList.BlazorWasm.Domain.Interfaces
{
    public interface ITodosRepository : IRepository<Entities.TodosList>
    {
        Task<IEnumerable<TodosList>> GetTodosAsync();

        Task<IReadOnlyList<TodosList>> GetTodosByUserIdAsync(Guid id);

        Task<TodosList> GetByIdAsync(int id);

        Task<TodosList> CreateTodoAsync(TodosList todo);

        Task<TodosList> UpdateTodoAsync(TodosList todo);

        Task<bool> DeleteTodoAsync(int id);
    }
}
