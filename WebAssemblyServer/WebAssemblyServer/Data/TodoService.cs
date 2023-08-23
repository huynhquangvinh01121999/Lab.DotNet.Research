using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAssemblyServer.Contexts;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Data
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _dbContext;
        private HttpClient _httpClient;

        public TodoService(AppDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
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
            //var response = await _httpClient.GetAsync("/users");

            //if (response.IsSuccessStatusCode)
            //{
            //    string result = await response.Content.ReadAsStringAsync();
            //}

            string json = JsonConvert.SerializeObject(new { userId = 101, title = "abc", completed = false });
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/todos", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
            }

            //    var taskViewModel = JsonSerializer.Deserialize<TaskViewModel>(returnValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
