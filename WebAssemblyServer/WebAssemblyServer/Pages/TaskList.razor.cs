using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblyServer.Components;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Pages
{
    public partial class TaskList
    {
        [Inject] private ITodoService todoService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        // khởi tạo object
        private Modal Modal { get; set; }
        private Confirmation RemoveConfirmation { get; set; }

        // khởi tạo biến
        private IEnumerable<TodosList> Tasks;
        private int DeleteId;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await todoService.GetTodosAsync();
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
                var result = await todoService.DeleteTodoAsync(DeleteId);
                if (result)
                    _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true); // reload page
            }
        }
    }
}
