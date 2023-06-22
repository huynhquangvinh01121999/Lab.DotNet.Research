using Identity.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.Extensions
{
    public static class ClientServiceExtensions
    {
        public static void AddClientIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<HangfireContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            // Add Identity
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<HangfireContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name = "CookieHangfire";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
            });
        }
    }
}
