using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Features.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var jwt = await _accountService.AuthenticateAsync(request);
            return Ok(jwt);
        }
    }
}