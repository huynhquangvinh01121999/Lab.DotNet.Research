using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Models;
using TodoListBlazorWasm.Services.TodoListServices;

namespace TodoListBlazorWasm.Pages
{
    public partial class TaskList
    {
        // khởi tạo các interface, library,...
        [Inject] private ITaskApiClient _taskApiClient { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        // khởi tạo object
        private Modal Modal { get; set; }
        private Confirmation RemoveConfirmation { get; set; }

        // khởi tạo biến
        private IReadOnlyList<TaskViewModel> Tasks;
        private int DeleteId;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await _taskApiClient.GetTodosAsync();
        }

        private void OnRemoveTask(int id)
        {
            DeleteId = id;
            RemoveConfirmation.Show();
        }

        private async Task OnRemoveTaskAsync(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                var result = await _taskApiClient.DeleteTaskAsync(DeleteId);
                if (result)
                    _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true); // reload page
            }
        }
    }
}
