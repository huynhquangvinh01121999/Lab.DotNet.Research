using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Pages
{
    public partial class UpdateTask
    {
        [Inject] private ITodoService todoService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        private TodosList Task;

        protected override async Task OnInitializedAsync()
        {
            Task = await todoService.GetByIdAsync(int.Parse(TaskId));
        }

        private async Task UpdateATask(EditContext context)
        {
            var result = await todoService.UpdateTodoAsync(Task);
            if (result != null)
                _navigationManager.NavigateTo("/tasks");
        }

        private void BackToList()
        {
            _navigationManager.NavigateTo("/tasks");
        }
    }
}
