using Application.Features.Accounts;
using Application.Features.Emails;
using Application.Features.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
