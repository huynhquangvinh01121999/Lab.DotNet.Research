using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;

namespace TodoListBlazorWasm.Services.TodoListServices
{
    public class TaskApiClient : ITaskApiClient
    {
        private HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TaskViewModel> CreateNewTaskAsync(CreateNewTaskRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Todos", request);

            var returnValue = await response.Content.ReadAsStringAsync();

            var taskViewModel = JsonSerializer.Deserialize<TaskViewModel>(returnValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return taskViewModel;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Todos/{id}");

            var returnValue = await response.Content.ReadAsStringAsync();

            return bool.Parse(returnValue);
        }

        public async Task<TaskViewModel> GetTodoByIdAsync(int id) 
            => await _httpClient.GetFromJsonAsync<TaskViewModel>($"/api/Todos/{id}");
            

        public async Task<IReadOnlyList<TaskViewModel>> GetTodosAsync() 
            => await _httpClient.GetFromJsonAsync<IReadOnlyList<TaskViewModel>>("/api/Todos");

        public async Task<bool> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/Todos", request);

            var returnValue = await response.Content.ReadAsStringAsync();

            return bool.Parse(returnValue);
        }
    }
}
