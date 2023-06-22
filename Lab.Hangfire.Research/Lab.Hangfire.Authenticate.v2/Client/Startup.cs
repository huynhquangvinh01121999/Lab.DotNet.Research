using Application;
using Client.JobHandle;
using Client.Middlewares;
using Hangfire;
using Identity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Client
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
            services.AddControllersWithViews();

            services.AddClientIdentityInfrastructure(_configuration);

            services.AddHttpContextAccessor();

            services.AddApplicationLayer();

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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var options = new DashboardOptions
            {
                //AppPath = _configuration.GetValue<string>("HangfireAuthenticate:AppPath"), 
                //AppPath = _configuration.GetSection("HangfireAuthenticate:AppPath").Value, 
                AppPath = "https://localhost:5001/Home/Logout", //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
                DashboardTitle = "My Dashboard Hangfire",
                Authorization = new[] { new MyAuthorizationFilter() }
            };

            // use HangfireDashboard
            app.UseHangfireDashboard("/Home/hangfire", options);

            recurringJobManager.AddOrUpdate("job1", () => Console.WriteLine($"Welcome {DateTime.Now.ToString("dd-MM-yyyy")}"), "*/1 * * * *");
            recurringJobManager.RegisterJobs(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
