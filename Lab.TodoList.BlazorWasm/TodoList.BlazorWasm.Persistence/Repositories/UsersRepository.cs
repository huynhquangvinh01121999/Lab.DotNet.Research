using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces;
using TodoList.BlazorWasm.Persistence.Contexts;
using TodoList.BlazorWasm.Persistence.Repositories.Base;

namespace TodoList.BlazorWasm.Persistence.Repositories
{
    public class UsersRepository : Repository<AppUsers>,IUsersRepository
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public UsersRepository(UserManager<AppUsers> userManager, IConfiguration configuration, AppDbContext dbContext) 
            : base (dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            // kiểm tra tài khoản có tồn tại hay không?
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                throw new Exception("User not exist!");

            // kiểm tra mật khẩu có chính xác hay không
            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("Password not correct!");

            //----------------------- token handler ----------------------------
            // lấy ra các claims
            var userClaims = await _userManager.GetClaimsAsync(user);

            // lấy ra các quyền của user
            var userRoles = await _userManager.GetRolesAsync(user);

            // khởi tạo danh sách chứa các claim
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            // thêm claim vào danh sách
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // generate access token
            var accessToken = generateAccessToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        public async Task<Guid> RegisterUserAsync(AppUsers user, string password)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);

            if (userExists != null)
                throw new Exception("User has been existed!");

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                string message = "";
                foreach (var item in result.Errors)
                {
                    message += item.Description + " ";
                }
                throw new Exception(message);
            }

            return user.Id;
        }

        // generate token
        private JwtSecurityToken generateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SECRET"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:VALID_ISSUER"],
                audience: _configuration["JWT:VALID_AUDIENCE"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
