using Application.Features.Users;
using Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
                return RedirectToAction("hangfire");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckLogin(string username, string pwd)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
                Response.Redirect("/Home/Index");

            var user = await _userManager.FindByNameAsync(username);
            if (await _userManager.CheckPasswordAsync(user, pwd) == false)
                return RedirectToAction("Index");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, pwd, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                Response.Redirect("/Home/Index");

            //-----------------------------------------------------------
            var userClaims = await _userManager.GetClaimsAsync(user);   // get list claims of user
            var roles = await _userManager.GetRolesAsync(user);     // get list roles of user

            // check user có role ADMIN hay không?
            var hasRoleAdmin = roles.Where(x => x.Equals("Admin")).FirstOrDefault(); // neu co role "Admin" moi duoc phep truy cap Dashboard Hangfire
            if(hasRoleAdmin == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var claimsIdentity = new ClaimsIdentity(claims, "ClaimsIdentity");

            var claimsPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
            //-----------------------------------------------------------
            await _signInManager.SignInWithClaimsAsync(user, false, claims);
            HttpContext.User = new ClaimsPrincipal(claimsPrincipal);
            //-----------------------------------------------------------
            return RedirectToAction("hangfire");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
