using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        public readonly ILdapService _ldapService;

        public AccountController(IAccountService accountService, INhanVienRepositoryAsync nhanVienRepositoryAsync,
            ILdapService ldapService)
        {
            _accountService = accountService;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _ldapService = ldapService;
        }

        [HttpPost("authenticateWithoutLdap")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var authenResult = await _accountService.AuthenticateAsync(request, GenerateIPAddress());
            if(authenResult.Succeeded == true)
            {
                var nhanvienAvatar = await _nhanVienRepositoryAsync.GetAvatarByUserIdAsync(authenResult.Data.Id);
                authenResult.Data.Avatar = nhanvienAvatar; 
            }
            return Ok(authenResult);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateWithLdapAsync(AuthenticationRequest request)
        {
            var authenResult = await _ldapService.AuthenticateAsync(request, GenerateIPAddress());

            if (authenResult.Succeeded == true)
            {
                if (authenResult.Data.FirstName != null && authenResult.Data.FirstName != "")
                {
                    var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(Guid.Parse(authenResult.Data.FirstName));
                    if (nhanvien != null)
                    {
                        authenResult.Data.FirstName = nhanvien.TenVN;
                        authenResult.Data.LastName = nhanvien.HoTenDemVN;
                        authenResult.Data.Avatar = nhanvien.Avatar;
                        authenResult.Data.Email = nhanvien.EmailCongTy;
                        authenResult.Data.UserName = nhanvien.MaNhanVien;
                    }
                    else
                    {
                        return Ok(new Response<AuthenticationResponse>("NhanVien not found.!"));
                    }
                }
                else
                {
                    return Ok(new Response<AuthenticationResponse>("NhanVienId not config.!"));
                }
            }

            return Ok(authenResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
        {
            if (refreshToken.AccessToken == "" || refreshToken.RefreshToken == "")
            {
                return BadRequest("Token or RefreshToken empty!");
            }
            else
            {
                return Ok(await _accountService.RefreshToken(refreshToken));
            }
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}