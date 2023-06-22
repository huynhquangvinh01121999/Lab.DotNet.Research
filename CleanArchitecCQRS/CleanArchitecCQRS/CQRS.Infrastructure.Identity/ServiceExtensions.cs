using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Infrastructure.Identity.Helpers;
using EsuhaiHRM.Infrastructure.Identity.Models;
using EsuhaiHRM.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Identity
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ILdapService, LdapService>();
            #endregion
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<HrmApp>(configuration.GetSection("HrmApp"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        //OnAuthenticationFailed = c =>
                        //{
                        //    if (c.Exception is SecurityTokenExpiredException)
                        //    {
                        //        // if you end up here, you know that the token is expired
                        //        c.Response.StatusCode = 401;
                        //        c.Response.ContentType = "application/json";
                        //        var result = JsonConvert.SerializeObject(new Response<string>("Token is expired"));
                        //        return c.Response.WriteAsync(result);
                        //    }
                        //    c.NoResult();
                        //    c.Response.StatusCode = 500;
                        //    c.Response.ContentType = "text/plain";
                        //    return c.Response.WriteAsync(c.Exception.ToString());
                        //},
                        //OnChallenge = context =>
                        //{
                        //    if (!context.Response.HasStarted)
                        //    {
                        //        context.HandleResponse();
                        //        context.Response.StatusCode = 401;
                        //        context.Response.ContentType = "application/json";
                        //    }
                        //    var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                        //    return context.Response.WriteAsync(result);
                        //},
                        //OnForbidden = context =>
                        //{
                        //    if (!context.Response.HasStarted)
                        //    {
                        //        context.Response.StatusCode = 403;
                        //        context.Response.ContentType = "application/json";
                        //    }
                        //    var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                        //    return context.Response.WriteAsync(result);
                        //},
                    };
                });
        }
    }
}
