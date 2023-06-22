using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface ILdapService
    {
        Task<bool> LdapLogin(string username, string password);
        Task<LdapUserInfo> LdapGetInfoAccount(string username);
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest refreshToken);
    }
}
