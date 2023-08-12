using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAssemblyServer.Contexts;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Data
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _dbContext;

        public TodoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodosList> CreateTodoAsync(TodosList todo)
        {
            await _dbContext.TodoLists.AddAsync(todo);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? todo : null;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _dbContext.TodoLists.FindAsync(id);

            if (todo == null)
                return false;

            _dbContext.TodoLists.Remove(todo);

            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<TodosList> GetByIdAsync(int id)
        {
            var result = from s in _dbContext.TodoLists
                         where s.Id == id
                         select s;
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TodosList>> GetTodosAsync()
        {
            var results = from s in _dbContext.TodoLists
                          select s;
            return await results
                .OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        public async Task<TodosList> UpdateTodoAsync(TodosList todo)
        {
            _dbContext.TodoLists.Update(todo);

            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? todo : null;
        }
    }
}
