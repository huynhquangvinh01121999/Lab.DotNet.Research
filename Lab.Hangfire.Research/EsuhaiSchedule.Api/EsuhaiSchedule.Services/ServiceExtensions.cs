using EsuhaiSchedule.Services.Interfaces;
using EsuhaiSchedule.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EsuhaiSchedule.Services
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // Add Dependency Injection
            //services.AddScoped<IEmailServices, EmailServices>();
            //services.AddTransient<IHangFireActiveJob, HangFireActiveJob>();
            services.AddTransient<IEmailServices, EmailServices>();
        }
    }
}
