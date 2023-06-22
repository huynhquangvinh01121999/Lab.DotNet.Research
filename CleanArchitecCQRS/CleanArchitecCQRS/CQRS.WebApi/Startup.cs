using EsuhaiHRM.Application;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Infrastructure.Identity;
using EsuhaiHRM.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EsuhaiHRM.WebApi.Extensions;
using EsuhaiHRM.WebApi.Services;
using System;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using System.IO;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Http;
using EsuhaiHRM.Infrastructure.Persistence.Repositories;
using EsuhaiHRM.Domain.Settings;
using CQRS.Infrastructure.Shared;

namespace EsuhaiHRM.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);
            services.AddSwaggerExtension();
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddCors(options =>
            {
                // WithOrigins("http://localhost:4200", "http://localhost:4201")
                options.AddPolicy(name: AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(_config["HrmApp:CorsOrigins"].Split(','))
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            services.AddControllersWithViews(op => { op.Filters.Add(typeof(TrackingUserActivityService)); }
            ).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors(AllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"" + FileDirection.Resources)
                )
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
