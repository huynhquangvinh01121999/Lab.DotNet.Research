using Application.DTOs.Account;
using System.Threading.Tasks;

namespace Application.Features.Accounts
{
    public interface IAccountService
    {
        Task<string> AuthenticateAsync(AuthenticationRequest request);
    }
}
