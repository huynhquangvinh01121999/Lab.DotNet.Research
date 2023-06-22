using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;
using TodoListBlazorWasm.Pages;
using TodoListBlazorWasm.Services.TodoListServices;
using TodoListBlazorWasm.Services.TypesServices;

namespace TodoListBlazorWasm.Components
{
    public partial class Modal
    {
        [Inject] private ITypeApiClient _typeApiClient { get; set; }
        [Inject] private ITaskApiClient _taskApiClient { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        private IReadOnlyList<TypeViewModel> Types;

        // tạo 1 model để binding data từ view xuống
        private CreateNewTaskRequest requestModel = new CreateNewTaskRequest();

        protected override async Task OnInitializedAsync()
        {
            Types = await _typeApiClient.GetTypesAsync();
        }

        private async Task CreateNewTask(EditContext context)
        {
            var result = await _taskApiClient.CreateNewTaskAsync(requestModel);

            if (result != null)
            {
                requestModel = new CreateNewTaskRequest();
                _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true); // reload page
                //Close();
            }
        }

    }
}
