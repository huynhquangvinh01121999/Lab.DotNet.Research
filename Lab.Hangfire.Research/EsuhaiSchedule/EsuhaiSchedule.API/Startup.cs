using AutoMapper;
using EsuhaiSchedule.API.Jobs;
using EsuhaiSchedule.Application;
using EsuhaiSchedule.Persistence;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Reflection;

namespace EsuhaiSchedule.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter()); // convert enums hien thi duoi dang so sang chuoi
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1"
                });
                //c.OperationFilter<ParameterFilter>();
                //c.IncludeXmlComments(string.Format(@"{0}\EsuhaiSchedule.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "EsuhaiSchedule.API.xml");
                c.IncludeXmlComments(filePath);
            }).AddSwaggerGenNewtonsoftSupport(); // convert enums hien thi duoi dang so sang chuoi

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddPersistenceLayer(Configuration);
            services.AddApplicationLayer(Configuration);

            // Add Hangfire services.
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            // Add processing server as IHostedService
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IRecurringJobManager recurringJobManager, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EsuhaiSchedule.Hangfire");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var options = new DashboardOptions
            {
                IgnoreAntiforgeryToken = true,
                AppPath = Configuration["HangfireAuthenticate:AppPath"],
                DashboardTitle = "My Hangfire Dashboard",
                Authorization = new[] {
                    new HangfireCustomBasicAuthenticationFilter() {
                        User = Configuration["HangfireAuthenticate:Username"],
                        Pass = Configuration["HangfireAuthenticate:Password"],
                    }
                },
            };

            // use HangfireDashboard
            app.UseHangfireDashboard("/hangfire", options);

            // register job
            //recurringJobManager.RegisterJobSendMail(app);
            //recurringJobManager.RegisterJobTongHopXetDuyet(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
