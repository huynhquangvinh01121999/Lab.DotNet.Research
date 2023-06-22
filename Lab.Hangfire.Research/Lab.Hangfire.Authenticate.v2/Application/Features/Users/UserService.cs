using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(IHttpContextAccessor accessor, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.accessor = accessor;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }
}
