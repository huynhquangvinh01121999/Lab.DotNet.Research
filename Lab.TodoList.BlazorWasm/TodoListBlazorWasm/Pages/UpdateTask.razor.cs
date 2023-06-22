using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;
using TodoListBlazorWasm.Services.TodoListServices;
using TodoListBlazorWasm.Services.TypesServices;

namespace TodoListBlazorWasm.Pages
{
    public partial class UpdateTask
    {
        [Inject] private ITaskApiClient _taskApiClient { get; set; }
        [Inject] private ITypeApiClient _typeApiClient { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        private UpdateTaskRequest modelUpdate;
        private TaskViewModel Task;

        private IReadOnlyList<TypeViewModel> Types;

        protected override async Task OnInitializedAsync()
        {
            Task = await _taskApiClient.GetTodoByIdAsync(int.Parse(TaskId));
            Types = await _typeApiClient.GetTypesAsync();
            modelUpdate = new UpdateTaskRequest
            {
                Id = Task.Id,
                Title = Task.Title,
                Content = Task.Content,
                StartDate = Task.StartDate,
                EndDate = Task.EndDate,
                Mode = Task.Mode,
                TypeId = Task.TypesId,
                UserId = Task.UserId
            };
        }

        private async Task UpdateATask(EditContext context)
        {
            var result = await _taskApiClient.UpdateTaskAsync(modelUpdate);
            if (result)
                _navigationManager.NavigateTo("/tasks");
        }

        private void BackToList()
        {
            _navigationManager.NavigateTo("/tasks");
        }
    }
}
