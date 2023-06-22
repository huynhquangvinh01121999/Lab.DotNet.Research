using System.Security.Claims;

namespace Application.Features.Users
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}
