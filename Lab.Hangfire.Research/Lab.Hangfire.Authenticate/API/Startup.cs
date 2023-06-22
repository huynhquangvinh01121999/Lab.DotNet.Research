using API.Authorization;
using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Hangfire services.
            services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));

            // Add processing server as IHostedService
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IRecurringJobManager recurringJobManager, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var options = new DashboardOptions
            {
                IgnoreAntiforgeryToken = true,
                AppPath = _configuration["HangfireAuthenticate:AppPath"],
                DashboardTitle = "My Hangfire Dashboard",
                Authorization = new[] {
                    new HangfireCustomBasicAuthenticationFilter() {
                        User = _configuration["HangfireAuthenticate:Username"],
                        Pass = _configuration["HangfireAuthenticate:Password"],
                    }
                },
            };

            // use HangfireDashboard
            app.UseHangfireDashboard("/hangfire", options);

            recurringJobManager.AddOrUpdate("job1", () => Console.WriteLine($"Welcome {DateTime.Now.ToString("dd-MM-yyyy")}"), "*/1 * * * *");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
