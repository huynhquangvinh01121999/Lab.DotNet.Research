using AutoMapper;
using EsuhaiSchedule.Application.Services.Email;
using EsuhaiSchedule.Application.Services.RecurringJob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EsuhaiSchedule.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Repositories
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IRecurringJobService, RecurringJobService>();
        }
    }
}
