using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebAssemblyServer.Data.Interfaces;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Components
{
    public partial class Modal
    {
        [Inject] private ITodoService todoService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        // tạo 1 model để binding data từ view xuống
        private TodosList requestModel = new TodosList();

        protected override Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        private async Task CreateNewTask(EditContext context)
        {
            var result = await todoService.CreateTodoAsync(requestModel);

            if (result != null)
            {
                requestModel = new TodosList();
                _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true); // reload page
                //Close();
                //_navigationManager.NavigateTo("/tasks");
            }
        }

        // modal handle
        public string ModalDisplay = "none;";
        public string ModalClass = "";
        public bool ShowBackdrop = false;

        public void Open()
        {
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public void Close()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }

    }
}
