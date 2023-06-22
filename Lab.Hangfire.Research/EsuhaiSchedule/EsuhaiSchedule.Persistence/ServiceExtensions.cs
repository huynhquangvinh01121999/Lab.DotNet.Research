using EsuhaiSchedule.Application.IRepositories;
using EsuhaiSchedule.Persistence.Context;
using EsuhaiSchedule.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsuhaiSchedule.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add Repositories
            services.AddTransient<ITongHopDuLieuRepositoryAsync, TongHopDuLieuRepositoryAsync>();
        }
    }
}
