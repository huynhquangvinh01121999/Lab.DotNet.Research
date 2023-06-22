using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace lab.hangfire.net.Extensions
{
    public static class HangfireExtensions
    {
        public static void AddHangfireExtensions(this IServiceCollection services, IConfiguration config)
        {
            // Add Hangfire services.
            services.AddHangfire(x => x.UseSqlServerStorage(config.GetConnectionString("DefaultConnection")));

            // Add processing server as IHostedService
            services.AddHangfireServer();

            /* tham khao: https://discuss.hangfire.io/t/how-does-the-following-sqlserverstorageoptions-work/6127/3
                          https://www.imaginet.com/2022/using-hangfire-run-background-jobs-iis/ */
            //services.AddHangfire(configuration => configuration
            //   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //   .UseSimpleAssemblyNameTypeSerializer()
            //   .UseRecommendedSerializerSettings()
            //   .UseSqlServerStorage(config.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            //   {
            //       CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //       SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //       QueuePollInterval = TimeSpan.Zero,  // cu sau mot khoang tg quy dinh, he thong se truy cap CSDL de tim xem co job nao can lam khong 
            //       UseRecommendedIsolationLevel = true,
            //       UsePageLocksOnDequeue = true,
            //       DisableGlobalLocks = true,
            //       PrepareSchemaIfNecessary = false
            //   }));

            // Add processing server as IHostedService
            // giảm số lượng bộ xử lý công việc xuống chỉ còn 1 nửa số lượng bộ xử lý trên máy chủ để không gây quá tải cho máy chủ web.
            //services.AddHangfireServer(options => { options.WorkerCount = Environment.ProcessorCount * 2; });
        }

        public static void UseHangfireExtensions(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            //app.UseHangfireServer();
            //app.UseHangfireDashboard("/hangfire");

            /* Multiple Dashboards */
            //var storage1 = new SqlServerStorage("Connection1");
            //var storage2 = new SqlServerStorage("Connection2");
            //app.UseHangfireDashboard("/hangfire1", new DashboardOptions(), storage1);
            //app.UseHangfireDashboard("/hangfire2", new DashboardOptions(), storage2);

            /* Change `Back to site` link URL */
            //var options = new DashboardOptions
            //{
            //    AppPath = "https://localhost:5001/api",
            //    StatsPollingInterval = 600000,
            //    Authorization = new[] { new LocalRequestsOnlyAuthorizationFilter() }
            //};
            //app.UseHangfireDashboard("/hangfire", options);
        }
    }
}
