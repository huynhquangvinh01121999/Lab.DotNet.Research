using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Infrastructure.Identity.Services;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repositories;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<ICongTyRepositoryAsync, CongTyRepositoryAsync>();
            services.AddTransient<ICaLamViecRepositoryAsync, CaLamViecRepositoryAsync>();
            services.AddTransient<IChucVuRepositoryAsync, ChucVuRepositoryAsync>();
            services.AddTransient<INhanVienRepositoryAsync, NhanVienRepositoryAsync>();

            services.AddTransient<IGroupMailRepositoryAsync, GroupMailRepositoryAsync>();
            services.AddTransient<IKhoaDaoTaoRepositoryAsync, KhoaDaoTaoRepositoryAsync>();
            services.AddTransient<IPhongBanRepositoryAsync, PhongBanRepositoryAsync>();
            services.AddTransient<ITinhThanhRepositoryAsync, TinhThanhRepositoryAsync>();
            services.AddTransient<IDanhMucRepositoryAsync, DanhMucRepositoryAsync>();

            services.AddTransient<IChiNhanhRepositoryAsync, ChiNhanhRepositoryAsync>();
            services.AddTransient<ITinNhanRepositoryAsync, TinNhanRepositoryAsync>();
            services.AddTransient<IDashBoardRepositoryAsync, DashBoardRepositoryAsync>();
            services.AddTransient<IGenerateService, GenerateRepositoryAsync>();
            services.AddTransient<IAdminRepositoryAsync, AdminRepositoryAsync>();
            services.AddTransient<ITimesheetRepositoryAsync, TimesheetRepositoryAsync>();
            services.AddTransient<ITongHopDuLieuRepositoryAsync, TongHopDuLieuRepositoryAsync>();
            services.AddTransient<ITangCaRepositoryAsync, TangCaRepositoryAsync>();
            services.AddTransient<IPhuCapRepositoryAsync, PhuCapRepositoryAsync>();
            services.AddTransient<ILoaiPhuCapRepositoryAsync, LoaiPhuCapRepositoryAsync>();
            services.AddTransient<INghiLeRepositoryAsync, NghiLeRepositoryAsync>();
            services.AddTransient<ICauHinhNgayCongRepositoryAsync, CauHinhNgayCongRepositoryAsync>();
            services.AddTransient<INghiPhepRepositoryAsync, NghiPhepRepositoryAsync>();
            services.AddTransient<IViecBenNgoaiRepositoryAsync, ViecBenNgoaiRepositoryAsync>();
            services.AddTransient<IDiemDenRepositoryAsync, DiemDenRepositoryAsync>();
            #endregion
        }
    }
}
