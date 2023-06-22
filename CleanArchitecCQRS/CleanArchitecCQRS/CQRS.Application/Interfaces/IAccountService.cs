using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.DTOs.Email;
using EsuhaiHRM.Application.Wrappers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<Response<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest refreshToken);
        Task SendMail(EmailRequest request);
        Task<Response<LoginUser>> GetLoginUser(string email);
    }
}
