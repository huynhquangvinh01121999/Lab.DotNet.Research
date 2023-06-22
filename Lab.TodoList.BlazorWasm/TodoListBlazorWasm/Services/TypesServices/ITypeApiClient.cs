using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;

namespace TodoListBlazorWasm.Services.TypesServices
{
    public interface ITypeApiClient
    {
        Task<IReadOnlyList<TypeViewModel>> GetTypesAsync();
    }
}
