using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.Enums;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Infrastructure.Identity.Helpers;
using EsuhaiHRM.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Identity.Services
{
    class LdapService : ILdapService
    {
        string ldap_host = "";
        int ldap_port = 0;
        string ldap_domain = "";
        string ldap_admin_username = "";
        string ldap_admin_password = "";
        string ldap_searchBase = "";
        string ldap_isEnabled = "";

        public IConfiguration _config { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _idenContext;
        private readonly DbSet<RefreshToken> _refreshToken;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        
        public LdapService(IConfiguration configuration, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IdentityContext identityContext, IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _idenContext = identityContext;
            _refreshToken = _idenContext.Set<RefreshToken>();
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            _config = configuration;

            ldap_host = _config["Ldap:Host"];
            ldap_port = _config["Ldap:Port"] != null ? Convert.ToInt32(_config["Ldap:Port"]) : LdapConnection.DefaultPort;
            ldap_domain = _config["Ldap:Domain"];
            ldap_admin_username = _config["Ldap:UserName"];
            ldap_admin_password = _config["Ldap:Password"];
            ldap_searchBase = _config["Ldap:Container"];
            ldap_isEnabled = _config["Ldap:IsEnabled"];
        }

        public Task<LdapUserInfo> LdapGetInfoAccount(string username)
        {
            LdapUserInfo user = new LdapUserInfo();
            try
            {
                LdapConnection conn = new LdapConnection();

                conn.Connect(ldap_host, ldap_port);
                conn.Bind(ldap_domain + '\\' + ldap_admin_username, ldap_admin_password);

                string[] requiredAttributes = { "cn", "sn", "uid", "userPrincipalName", "sAMAccountName", "givenName" };
                string ldap_searchValue = string.Format("(userPrincipalName={0}@*)", username);
                
                ILdapSearchResults lsc = conn.Search(ldap_searchBase, LdapConnection.ScopeSub, ldap_searchValue, requiredAttributes, false);
                
                while (lsc.HasMore())
                {
                    LdapEntry nextEntry = null;
                    try
                    {
                        nextEntry = lsc.Next();
                    }
                    catch (LdapException e)
                    {
                        continue;
                    }
                    LdapAttributeSet attributeSet = nextEntry.GetAttributeSet();
                    System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                    while (ienum.MoveNext())
                    {
                        LdapAttribute attribute = (LdapAttribute)ienum.Current;
                        switch(attribute.Name)
                        {
                            case "cn":
                                user.FullName = attribute.StringValue;
                                break;
                            case "sn":
                                user.FirstName = attribute.StringValue;
                                break;
                            case "givenName":
                                user.LastName = attribute.StringValue;
                                break;
                            case "sAMAccountName":
                                user.Username = attribute.StringValue;
                                break;
                            case "userPrincipalName":
                                user.Email = attribute.StringValue;
                                break;
                        }
                    }
                }
                conn.Disconnect();

                return Task.FromResult(user);
            }
            catch (Exception e)
            {
                return Task.FromResult(user);
            }
        }
        public Task<bool> LdapLogin(string username, string password)
        {
            try
            {
                LdapConnection conn = new LdapConnection();

                conn.Connect(ldap_host, ldap_port);
                conn.Bind(ldap_domain + '\\' + username, password);

                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                return Task.FromResult(false);
            }
        }
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            try
            {
                bool IsEnabled = Convert.ToBoolean(ldap_isEnabled);
                if (IsEnabled)
                    return await AuthenticateWithLdapAsync(request, ipAddress);
                else
                    return await AuthenticateWithoutLdapAsync(request, ipAddress);
            }
            catch(Exception ex)
            {
                return new Response<AuthenticationResponse>("System error");
                //throw new ApiException($"System error");
            }
        }


        public async Task<Response<AuthenticationResponse>> AuthenticateWithLdapAsync(AuthenticationRequest request, string ipAddress)
        {
            if (LdapLogin(request.Email, request.Password).Result == true)
            {
                var user = await _userManager.FindByNameAsync(request.Email);
                
                if (user == null)
                {
                    return new Response<AuthenticationResponse>($"No Accounts Registered with {request.Email}.");
                }
                if (!user.EmailConfirmed)
                {
                    return new Response<AuthenticationResponse>($"Account Not Confirmed for '{request.Email}'.");
                }
                
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.FirstName = user.NhanVienId.ToString();
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                var refreshToken = GenerateRefreshToken();
                response.RefreshToken = refreshToken.Token;
                refreshToken.ApplicationUserId = user.Id;
                await DisableExistsRefreshToken(user.Id);

                _refreshToken.Add(refreshToken);
                _idenContext.SaveChanges();

                return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");

                //var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                //response.Email = user.Email;
                //response.UserName = user.UserName;
                //response.FirstName = user.FirstName;
                //response.LastName = user.LastName;
                //response.IsVerified = user.EmailConfirmed;
            }
            else
            {
                return new Response<AuthenticationResponse>($"No Accounts Registered with {request.Email}.");
            }
        }
        public async Task<Response<AuthenticationResponse>> AuthenticateWithoutLdapAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Response<AuthenticationResponse>($"No Accounts Registered with {request.Email}.");
                //throw new ApiException($"No Accounts Registered with {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new Response<AuthenticationResponse>($"Invalid Credentials for '{request.Email}'.");
                //throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }
            if (!user.EmailConfirmed)
            {
                return new Response<AuthenticationResponse>($"Account Not Confirmed for '{request.Email}'.");
                //throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            //response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            //response.Avatar = "https://randomuser.me/api/portraits/women/1.jpg";
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;

            //add token history to RefreshToken
            refreshToken.ApplicationUserId = user.Id;

            await DisableExistsRefreshToken(user.Id);

            _refreshToken.Add(refreshToken);
            _idenContext.SaveChanges();

            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }
        
        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    //var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    //return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                    return new Response<string>(user.Id, message: $"User Registered");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email } is already registered.");
            }
        }
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays),
                //Expires = DateTime.Now.AddMinutes(3),
                Created = DateTime.Now,
                CreatedByIp = IpHelper.GetIpAddress()
            };
        }
        public async Task<Response<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest requestRefresh)
        {
            var principal = GetPrincipalFromExpiredToken(requestRefresh.AccessToken);

            var userResult = await (from re in _refreshToken.Where(re => re.Token == requestRefresh.RefreshToken)
                                    join us in _userManager.Users
                                    on re.ApplicationUserId equals us.Id
                                    select new { ReToken = re, User = us }).SingleOrDefaultAsync();

            if (userResult != null && userResult.ReToken.Expires <= DateTime.Now)
            {
                userResult.ReToken.ReplacedByToken = requestRefresh.AccessToken;
                userResult.ReToken.Revoked = DateTime.UtcNow;
                userResult.ReToken.RevokedByIp = IpHelper.GetIpAddress();
                _idenContext.SaveChanges();

                throw new ApiException("Login Expired.");
            }
            else
            {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(userResult.User);
                var newToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                RefreshTokenResponse refreshTokenResponse = new RefreshTokenResponse();
                refreshTokenResponse.Token = newToken;
                return new Response<RefreshTokenResponse>(refreshTokenResponse);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("User not Found.");
            return principal;
        }
        public async Task<int> DisableExistsRefreshToken(string userId)
        {
            try
            {
                var resultUpdate = await _refreshToken.Where(re => re.ApplicationUserId == userId
                                                               && (re.Revoked == null
                                                                || re.ReplacedByToken == null
                                                                || re.RevokedByIp == null))
                                                      .ToListAsync();

                for (int i = 0; i < resultUpdate.Count(); i++)
                {
                    resultUpdate[i].Revoked = DateTime.Now;
                    resultUpdate[i].ReplacedByToken = "AUTO";
                    resultUpdate[i].RevokedByIp = IpHelper.GetIpAddress();
                }
                return await _idenContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(-1);
            }
        }
        
    }
}
