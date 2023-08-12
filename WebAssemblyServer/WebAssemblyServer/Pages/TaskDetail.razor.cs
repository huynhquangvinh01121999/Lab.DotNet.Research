using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Pages
{
    public partial class TaskDetail
    {
        [Inject] private ITodoService todoService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string TodoListId { get; set; }

        private TodosList TaskVM;

        protected override async Task OnInitializedAsync()
        {
            TaskVM = await todoService.GetByIdAsync(int.Parse(TodoListId));
        }

        private void BackToList()
        {
            _navigationManager.NavigateTo("/tasks");
        }
    }
}
