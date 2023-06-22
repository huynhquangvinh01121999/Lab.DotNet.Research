using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoListBlazorWasm.Models;

namespace TodoListBlazorWasm.Services.TypesServices
{
    public class TypeApiClient : ITypeApiClient
    {
        private HttpClient _httpClient;

        public TypeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<TypeViewModel>> GetTypesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<TypeViewModel>>("/api/Types");
            return response;
        }
    }
}
