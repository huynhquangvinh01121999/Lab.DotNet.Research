using System;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces.Base;

namespace TodoList.BlazorWasm.Domain.Interfaces
{
    public interface IUsersRepository : IRepository<AppUsers>
    {
        Task<string> Authenticate(string username, string password);
        Task<Guid> RegisterUserAsync(AppUsers user,string password);
    }
}
