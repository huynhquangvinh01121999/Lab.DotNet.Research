using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Queries.GetCauHinhNgayCongs;
using EsuhaiHRM.Application.Features.NghiLes.Queries.GetNghiLes;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetDetailNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Domain.Common;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CongTy> CongTys { get; set; }
        public DbSet<CaLamViec> CaLamViecs { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<BenhVienBHXH> BenhVienBHXHs { get; set; }
        public DbSet<GroupMail> GroupMails { get; set; }
        public DbSet<KhoaDaoTao> KhoaDaoTaos { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<TinhThanh> TinhThanhs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<NhanVien_CongTy> NhanVien_CongTys { get; set; }
        public DbSet<NhanVien_GroupMail> NhanVien_GroupMails { get; set; }
        public DbSet<NhanVien_KhoaDaoTao> NhanVien_KhoaDaoTaos { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<NhanVien_VangMat> NhanVien_VangMats { get; set; }
        public DbSet<NhanVien_ThongTinGiaDinh> NhanVien_ThongTinGiaDinhs { get; set; }
        public DbSet<NhanVien_ThongTinHocVan> NhanVien_ThongTinHocVans { get; set; }
        public DbSet<NhanVien_BangCapChungChi> NhanVien_BangCapChungChis { get; set; }
        public DbSet<NhanVien_BaoHiem> NhanVien_BaoHiems { get; set; }
        public DbSet<NhanVien_CaLamViec> NhanVien_CaLamViecs { get; set; }
        public DbSet<NhanVien_DanhGia> NhanVien_DanhGias { get; set; }
        public DbSet<NhanVien_GiayPhep> NhanVien_GiayPheps { get; set; }
        public DbSet<NhanVien_HopDong> NhanVien_HopDongs { get; set; }
        public DbSet<NhanVien_HoSo> NhanVien_HoSos { get; set; }
        public DbSet<NhanVien_ThongTinKhac> NhanVien_ThongTinKhacs { get; set; }
        public DbSet<NhanVien_PhuCap> NhanVien_PhuCaps { get; set; }
        public DbSet<NhanVien_ThaiSan> NhanVien_ThaiSans { get; set; }
        public DbSet<NhanVien_ThoiViec> NhanVien_ThoiViecs { get; set; }
        public DbSet<NhanVien_CongTac> NhanVien_CongTacs { get; set; }
        public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<TinNhan> TinNhans { get; set; }
        public DbSet<NhanVien_TinNhan> NhanVien_TinNhans { get; set; }
        public DbSet<NhanVien_CMND> NhanVien_CMNDs { get; set; }
        public DbSet<BaoHiem> BaoHiems { get; set; }
        public DbSet<Huyen> Huyens { get; set; }
        public DbSet<Xa> Xas { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TongHopDuLieu> TongHopDuLieus { get; set; }
        public DbSet<TangCa> TangCas { get; set; }

        public DbSet<PhuCap> PhuCaps { get; set; }
        public DbSet<LoaiPhuCap> LoaiPhuCaps { get; set; }
        public DbSet<NghiPhep> NghiPheps { get; set; }
        public DbSet<ViecBenNgoai> ViecBenNgoais { get; set; }
        public DbSet<NghiLe> NghiLes { get; set; }
        public DbSet<CauHinhNgayCong> CauHinhNgayCongs { get; set; }
        public DbSet<DiemDen> DiemDens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var schemaNhanSu = Schemas.NHANSU;
            var schemeDefault = Schemas.ESUHAI;
            var schemeViecBenNgoai = Schemas.VIECBENNGOAI;

            // NamLe - Ignore - Stored Procedure
            builder.Ignore<GetDieuChinhTsDetailViewModel>();
            builder.Entity<GetDieuChinhTsDetailViewModel>().HasNoKey().ToView(null);

            builder.Ignore<DashBoard_12Months>();
            builder.Entity<DashBoard_12Months>().HasNoKey().ToView(null);

            builder.Ignore<DashBoard_DanhMuc>();
            builder.Entity<DashBoard_DanhMuc>().HasNoKey().ToView(null);

            builder.Ignore<DashBoard>();
            builder.Entity<DashBoard>().HasNoKey().ToView(null);

            builder.Ignore<GetTongHopDuLieusByNhanVienViewModel>();
            builder.Entity<GetTongHopDuLieusByNhanVienViewModel>().HasNoKey().ToView(null);
            //- NamLe - Ignore

            // VinhHuynh - Ignore - Stored Procedure
            builder.Ignore<GetPhuCapsHrViewModel>();
            builder.Entity<GetPhuCapsHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetPhuCapsNotHrViewModel>();
            builder.Entity<GetPhuCapsNotHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetPhuCapsByNhanVienViewModel>();
            builder.Entity<GetPhuCapsByNhanVienViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetTangCasNotHrViewModel>();
            builder.Entity<GetTangCasNotHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetTangCasHrViewModel>();
            builder.Entity<GetTangCasHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetTangCasByNhanVienViewModel>();
            builder.Entity<GetTangCasByNhanVienViewModel>().HasNoKey().ToView(null);

            //builder.Ignore<GetTimesheetsHrViewModel>();
            //builder.Entity<GetTimesheetsHrViewModel>().HasNoKey().ToView(null);
            builder.Ignore<GetTimesheetsHrViewResults>();
            builder.Entity<GetTimesheetsHrViewResults>().HasNoKey().ToView(null);

            builder.Ignore<GetTimesheetsNotHrViewModel>();
            builder.Entity<GetTimesheetsNotHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetTimesheetsPhongBanResults>();
            builder.Entity<GetTimesheetsPhongBanResults>().HasNoKey().ToView(null);

            builder.Ignore<QuanLyNgayCongViewModel>();
            builder.Entity<QuanLyNgayCongViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetTongHopNghiViewModel>();
            builder.Entity<GetTongHopNghiViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetDetailNghiPhepViewModel>();
            builder.Entity<GetDetailNghiPhepViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetNghiPhepsHrViewModel>();
            builder.Entity<GetNghiPhepsHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetNghiPhepsNotHrViewModel>();
            builder.Entity<GetNghiPhepsNotHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetDetailViecBenNgoaiViewModel>();
            builder.Entity<GetDetailViecBenNgoaiViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetViecBenNgoaisNotHrViewModel>();
            builder.Entity<GetViecBenNgoaisNotHrViewModel>().HasNoKey().ToView(null);

            builder.Ignore<GetViecBenNgoaiHrViewModel>();
            builder.Entity<GetViecBenNgoaiHrViewModel>().HasNoKey().ToView(null);
            //- VinhHuynh - Ignore

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            };
            foreach (var cascadeFKs in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                cascadeFKs.DeleteBehavior = DeleteBehavior.Restrict;
            };

            base.OnModelCreating(builder);

            builder.HasDefaultSchema(schemaNhanSu);

            builder.Entity<TinhThanh>(entity => {
                entity.ToTable(name: "TinhThanhs", schema: schemeDefault);
            });

            builder.Entity<DanhMuc>(entity => {
                entity.ToTable(name: "DanhMucs", schema: schemeDefault);
            });
            
            builder.Entity<DiemDen>(entity => {
                entity.ToTable(name: "DiemDens", schema: schemeViecBenNgoai);
            });

            builder.Entity<BenhVienBHXH>().HasOne<TinhThanh>(s => s.TinhThanh)
                                          .WithMany(g => g.BenhVienBHXHs)
                                          .HasForeignKey(s => s.TinhId);

            //Add foreign key for NhanVien - CongTy
            builder.Entity<NhanVien>().HasOne<CongTy>(ct => ct.TenCongTy)
                                      .WithMany(nv => nv.NhanViens)
                                      .HasForeignKey(ct => ct.CongTyId);

            //Add foreign key for NhanVien - ChucVu
            builder.Entity<NhanVien>().HasOne<ChucVu>(ct => ct.TenChucVu)
                                      .WithMany(nv => nv.ChucVuNhanViens)
                                      .HasForeignKey(ct => ct.ChucVuId);

            //Add foreign key for NhanVien - ChucDanh
            builder.Entity<NhanVien>().HasOne<ChucVu>(ct => ct.TenChucDanh)
                                      .WithMany(nv => nv.ChucDanhNhanViens)
                                      .HasForeignKey(ct => ct.ChucDanhId);

            //Add foreign key for NhanVien - NhanVien_CongTy
            builder.Entity<NhanVien_CongTy>().HasOne<NhanVien>(nvct => nvct.NhanVien)
                                             .WithMany(s => s.NhanVien_CongTys)
                                             .HasForeignKey(nvct => nvct.NhanVienId);

            //Add foreign key for CongTy - NhanVien_CongTy
            builder.Entity<NhanVien_CongTy>().HasOne<CongTy>(nvct => nvct.CongTy)
                                             .WithMany(s => s.NhanVien_CongTys)
                                             .HasForeignKey(nvct => nvct.CongTyId);

            //Add foreign key for PhongBan - NhanVien => get ten truong phong
            builder.Entity<PhongBan>().HasOne<NhanVien>(pb => pb.TruongBoPhan)
                                      .WithMany(nv => nv.TruongBoPhans)
                                      .HasForeignKey(pb => pb.TruongBoPhanId);

            //Add foreign key for PhongBan - GroupMail => get group mail
            builder.Entity<PhongBan>().HasOne<GroupMail>(pb => pb.GroupMail)
                                      .WithOne(nv => nv.MailPhongBan)
                                      .HasForeignKey<PhongBan>(pb => pb.GroupMailId);

            //Add foreign key for NhanVien - NhanVien_GroupMail
            builder.Entity<NhanVien_GroupMail>().HasOne<NhanVien>(nvgm => nvgm.NhanVien)
                                                .WithMany(s => s.NhanVien_GroupMails)
                                                .HasForeignKey(nvgm => nvgm.NhanVienId);

            //Add foreign key for GroupMail - NhanVien_GroupMail
            builder.Entity<NhanVien_GroupMail>().HasOne<GroupMail>(nvgm => nvgm.GroupMail)
                                                .WithMany(s => s.NhanVien_GroupMails)
                                                .HasForeignKey(nvgm => nvgm.GroupMailId);

            //Add foreign key for GroupMail - NhanVien [get NhanVienDeNghi]
            builder.Entity<GroupMail>().HasOne<NhanVien>(gm => gm.NhanVienDeNghi)
                                       .WithMany(gm => gm.DeNghiGroupMails)
                                       .HasForeignKey(gm => gm.NhanVienDeNghiId);

            //Add foreign key for KhoaDaoTao - NhanVien_KhoaDaoTao
            builder.Entity<NhanVien_KhoaDaoTao>().HasOne<NhanVien>(nvgm => nvgm.NhanVien)
                                                .WithMany(s => s.NhanVien_KhoaDaoTaos)
                                                .HasForeignKey(nvgm => nvgm.NhanVienId);

            //Add foreign key for KhoaDaoTao - NhanVien_KhoaDaoTao
            builder.Entity<NhanVien_KhoaDaoTao>().HasOne<KhoaDaoTao>(nvgm => nvgm.KhoaDaoTao)
                                                .WithMany(s => s.NhanVien_KhoaDaoTaos)
                                                .HasForeignKey(nvgm => nvgm.KhoaDaoTaoId);

            //Add foreign key for NhanVien - DanhMuc[GioiTinhs]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.GioiTinh)
                                      .WithMany(nv => nv.GioiTinhs)
                                      .HasForeignKey(ct => ct.GioiTinhId);
            //Add foreign key for NhanVien - DanhMuc[HonNhans]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.HonNhan)
                                      .WithMany(nv => nv.HonNhans)
                                      .HasForeignKey(ct => ct.HonNhanId);
            //Add foreign key for NhanVien - DanhMuc[TonGiaos]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.TonGiao)
                                      .WithMany(nv => nv.TonGiaos)
                                      .HasForeignKey(ct => ct.TonGiaoId);
            //Add foreign key for NhanVien - DanhMuc[NguyenQuans]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.NguyenQuan)
                                      .WithMany(nv => nv.NguyenQuans)
                                      .HasForeignKey(ct => ct.NguyenQuanId);
            //Add foreign key for NhanVien - DanhMuc[NoiSinhs]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.NoiSinh)
                                      .WithMany(nv => nv.NoiSinhs)
                                      .HasForeignKey(ct => ct.NoiSinhId);
            //Add foreign key for NhanVien - DanhMuc[QuocTichs]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.QuocTich)
                                      .WithMany(nv => nv.QuocTichs)
                                      .HasForeignKey(ct => ct.QuocTichId);

            //Add foreign key for NhanVien - DanhMuc[TrangThais]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.TrangThai)
                                      .WithMany(nv => nv.TrangThais)
                                      .HasForeignKey(ct => ct.TrangThaiId);

            //Add foreign key for NhanVien - DanhMuc[DanTocs]
            builder.Entity<NhanVien>().HasOne<DanhMuc>(ct => ct.DanToc)
                                      .WithMany(nv => nv.DanTocs)
                                      .HasForeignKey(ct => ct.DanTocId);

            //Add foreign key for NhanVien - PhongBan => get ten phong 
            builder.Entity<NhanVien>().HasOne<PhongBan>(nv => nv.TenPhong)
                                      .WithMany(pb => pb.NhanVienPhongs)
                                      .HasForeignKey(pb => pb.PhongId);

            //Add foreign key for NhanVien - PhongBan => get ten ban
            builder.Entity<NhanVien>().HasOne<PhongBan>(nv => nv.TenBan)
                                      .WithMany(pb => pb.NhanVienBans)
                                      .HasForeignKey(pb => pb.BanId);

            //Add foreign key for NhanVien - PhongBan => get ten nhom
            builder.Entity<NhanVien>().HasOne<PhongBan>(nv => nv.TenNhom)
                                      .WithMany(pb => pb.NhanVienNhoms)
                                      .HasForeignKey(pb => pb.NhomId);

            //Add foreign key for NhanVien - NhanVien_VangMat 
            builder.Entity<NhanVien_VangMat>().HasOne<NhanVien>(nv => nv.NhanViens)
                                              .WithMany(nvvm => nvvm.NhanVien_VangMats)
                                              .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien - NhanVien_ThongTinGiaDinh
            builder.Entity<NhanVien_ThongTinGiaDinh>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                      .WithMany(ttgd => ttgd.NhanVien_ThongTinGiaDinhs)
                                                      .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_ThongTinGiaDinh - DanhMuc[QuanHe]
            builder.Entity<NhanVien_ThongTinGiaDinh>().HasOne<DanhMuc>(ct => ct.QuanHe)
                                                      .WithMany(nv => nv.QuanHeGiaDinhs)
                                                      .HasForeignKey(ct => ct.QuanHeId);

            //Add foreign key for NhanVien - NhanVien_ThongTinHocVan
            builder.Entity<NhanVien_ThongTinHocVan>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(ttgd => ttgd.NhanVien_ThongTinHocVans)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_ThongTinGiaDinh - DanhMuc[Trinhdo]
            builder.Entity<NhanVien_ThongTinHocVan>().HasOne<DanhMuc>(ct => ct.TrinhDo)
                                                      .WithMany(nv => nv.TrinhDoHocVans)
                                                      .HasForeignKey(ct => ct.TrinhDoId);

            //Add foreign key for NhanVien_ThongTinGiaDinh - DanhMuc[NhomNganh]
            builder.Entity<NhanVien_ThongTinHocVan>().HasOne<DanhMuc>(ct => ct.NhomNganh)
                                                      .WithMany(nv => nv.NhomNganhs)
                                                      .HasForeignKey(ct => ct.NhomNganhId);

            //Add foreign key for NhanVien - NhanVien_BangCapChungChi
            builder.Entity<NhanVien_BangCapChungChi>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(ttgd => ttgd.NhanVien_BangCapChungChis)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for DanhMuc - NhanVien_BangCapChungChi
            builder.Entity<NhanVien_BangCapChungChi>().HasOne<DanhMuc>(nv => nv.LoaiBangCapChungChi)
                                                      .WithMany(ttgd => ttgd.LoaiBangCapChungChis)
                                                      .HasForeignKey(nv => nv.PhanLoaiId);

            //Add foreign key for NhanVien - NhanVien_BaoHiem
            builder.Entity<NhanVien_BaoHiem>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(ttgd => ttgd.NhanVien_BaoHiems)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien - NhanVien_BangCapChungChi
            builder.Entity<NhanVien_BaoHiem>().HasOne<BenhVienBHXH>(nv => nv.BenhVienBHXH)
                                                     .WithMany(nvbh => nvbh.NhanVien_BaoHiems)
                                                     .HasForeignKey(nv => nv.BenhVienId);

            //Add foreign key for NhanVien - CaLamViec
            builder.Entity<NhanVien>().HasOne<CaLamViec>(ct => ct.CaLamViec)
                                      .WithMany(nv => nv.NhanViens)
                                      .HasForeignKey(ct => ct.CaLamViecId);

            //Add foreign key for NhanVien_CaLamViec - NhanVien
            builder.Entity<NhanVien_CaLamViec>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(clv => clv.NhanVien_CaLamViecs)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_CaLamViec - NhanVien
            builder.Entity<NhanVien_CaLamViec>().HasOne<CaLamViec>(nv => nv.CaLamViec)
                                                     .WithMany(clv => clv.NhanVien_CaLamViecs)
                                                     .HasForeignKey(nv => nv.CaLamViecId);

            //Add foreign key for NhanVien_DanhGia - NhanVien
            builder.Entity<NhanVien_DanhGia>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(dg => dg.NhanVien_DanhGias)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_DanhGia - Danh muc [get ten loai danh gia]
            builder.Entity<NhanVien_DanhGia>().HasOne<DanhMuc>(nv => nv.LoaiDanhGia)
                                                     .WithMany(dg => dg.LoaiDanhGias)
                                                     .HasForeignKey(nv => nv.LoaiDanhGiaId);

            //Add foreign key for NhanVien_GiayPhep - NhanVien
            builder.Entity<NhanVien_GiayPhep>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(gp => gp.NhanVien_GiayPheps)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_HopDong - NhanVien
            builder.Entity<NhanVien_HopDong>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(hd => hd.NhanVien_HopDongs)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_HopDong - CongTy
            builder.Entity<NhanVien_HopDong>().HasOne<CongTy>(nv => nv.CongTy)
                                                     .WithMany(hd => hd.NhanVien_HopDongs)
                                                     .HasForeignKey(nv => nv.CongTyId);

            //Add foreign key for NhanVien_HopDong - DanhMuc [get ten loai hop dong]
            builder.Entity<NhanVien_HopDong>().HasOne<DanhMuc>(nv => nv.LoaiHopDong)
                                                     .WithMany(hd => hd.LoaiHopDongs)
                                                     .HasForeignKey(nv => nv.LoaiHopDongId);

            //Add foreign key for NhanVien_HoSo - NhanVien
            builder.Entity<NhanVien_HoSo>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                     .WithMany(hd => hd.NhanVien_HoSos)
                                                     .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_HoSo - DanhMuc[get ten ho so]
            builder.Entity<NhanVien_HoSo>().HasOne<DanhMuc>(nv => nv.TenHoSo)
                                                     .WithMany(hd => hd.HoSos)
                                                     .HasForeignKey(nv => nv.HoSoId);

            //Add foreign key for NhanVien_HoSo - DanhMuc[get ten loai ho so]
            builder.Entity<NhanVien_HoSo>().HasOne<DanhMuc>(nv => nv.TenLoaiHoSo)
                                                     .WithMany(hd => hd.LoaiHoSos)
                                                     .HasForeignKey(nv => nv.LoaiHoSoId);

            //Add foreign key for NhanVien_ThongTinKhac - NhanVien
            builder.Entity<NhanVien_ThongTinKhac>().HasOne<NhanVien>(nv => nv.NhanVien)
                                                   .WithMany(hd => hd.NhanVien_ThongTinKhacs)
                                                   .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_ThongTinKhac - DanhMuc
            builder.Entity<NhanVien_ThongTinKhac>().HasOne<DanhMuc>(nv => nv.LoaiXuatCanh)
                                                   .WithMany(hd => hd.LoaiXuatCanhs)
                                                   .HasForeignKey(nv => nv.PhanLoaiId);

            //Add foreign key for NhanVien_PhuCap - NhanVien
            builder.Entity<NhanVien_PhuCap>().HasOne<NhanVien>(nv => nv.NhanVien)
                                             .WithMany(hd => hd.NhanVien_PhuCaps)
                                             .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_PhuCap - DanhMuc [get LoaiPhuCap]
            builder.Entity<NhanVien_PhuCap>().HasOne<DanhMuc>(nv => nv.LoaiPhuCap)
                                             .WithMany(hd => hd.LoaiPhuCaps)
                                             .HasForeignKey(nv => nv.LoaiPhuCapId);

            //Add foreign key for NhanVien_ThaiSan - NhanVien
            builder.Entity<NhanVien_ThaiSan>().HasOne<NhanVien>(nv => nv.NhanVien)
                                              .WithMany(hd => hd.NhanVien_ThaiSans)
                                              .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_ThoiViec - NhanVien
            builder.Entity<NhanVien_ThoiViec>().HasOne<NhanVien>(nv => nv.NhanVien)
                                               .WithMany(hd => hd.NhanVien_ThoiViecs)
                                               .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key for NhanVien_CongTac - NhanVien
            builder.Entity<NhanVien_CongTac>().HasOne<NhanVien>(nv => nv.NhanVien)
                                              .WithMany(hd => hd.NhanVien_CongTacs)
                                              .HasForeignKey(nv => nv.NhanVienId);

            //add foreign key for nhanvien_congtac - chinhanh
            builder.Entity<NhanVien_CongTac>().HasOne<ChiNhanh>(nv => nv.TrucThuoc)
                                              .WithMany(hd => hd.NhanVien_TrucThuocs)
                                              .HasForeignKey(nv => nv.TrucThuocId);

            //add foreign key for nhanvien_congtac - chinhanh
            builder.Entity<NhanVien_CongTac>().HasOne<ChiNhanh>(nv => nv.NoiLamViec)
                                              .WithMany(hd => hd.NhanVien_NoiLamViecs)
                                              .HasForeignKey(nv => nv.NoiLamViecId);

            //add foreign key for nhanvien_congtac - chinhanh
            builder.Entity<NhanVien_CongTac>().HasOne<ChiNhanh>(nv => nv.CoSo)
                                              .WithMany(hd => hd.NhanVien_CoSos)
                                              .HasForeignKey(nv => nv.CoSoId);

            //add foreign key for nhanvien_congtac - danhmuc [get tinhtrangcongtac]
            builder.Entity<NhanVien_CongTac>().HasOne<DanhMuc>(nv => nv.TinhTrangCongTac)
                                              .WithMany(hd => hd.TinhTrangCongTacs)
                                              .HasForeignKey(nv => nv.TinhTrangCongTacId);

            //Add foreign key for ChiNhanh  - DanhMuc [get tenLoaiChiNhanh]
            builder.Entity<ChiNhanh>().HasOne<DanhMuc>(nv => nv.LoaiChiNhanh)
                                      .WithMany(hd => hd.LoaiChiNhanhs)
                                      .HasForeignKey(nv => nv.LoaiChiNhanhId);

            //Add foreign key for ChiNhanh  - TinhThanh [get tenTinhThanh]
            builder.Entity<ChiNhanh>().HasOne<TinhThanh>(nv => nv.TinhThanh)
                                      .WithMany(hd => hd.ChiNhanhs)
                                      .HasForeignKey(nv => nv.TinhId);

            //Add foreign key for ChiNhanh  - ChinhNhanh [get parent]
            builder.Entity<ChiNhanh>().HasOne<ChiNhanh>(nv => nv.TruSoChinh)
                                      .WithMany(hd => hd.ChiNhanhNhos)
                                      .HasForeignKey(nv => nv.Parent);

            //Add foreign key for NhanVien_CongTy - ChucVu[get Chuc Vu]
            builder.Entity<NhanVien_CongTy>().HasOne<ChucVu>(nv => nv.ChucVu)
                                             .WithMany(hd => hd.ChucVuNhanVien_CongTys)
                                             .HasForeignKey(nv => nv.ChucVuId);

            //Add foreign key for NhanVien_CongTy - ChucVu[get Chuc Danh]
            builder.Entity<NhanVien_CongTy>().HasOne<ChucVu>(nv => nv.ChucDanh)
                                             .WithMany(hd => hd.ChucDanhNhanVien_CongTys)
                                             .HasForeignKey(nv => nv.ChucDanhId);

            //Add foreign key for NhanVien_CongTy - PhongBan[get TenPhong]
            builder.Entity<NhanVien_CongTy>().HasOne<PhongBan>(nv => nv.TenPhong)
                                             .WithMany(hd => hd.PhongNhanVien_CongTy)
                                             .HasForeignKey(nv => nv.PhongId);

            //Add foreign key for NhanVien_CongTy - PhongBan[get TenBan]
            builder.Entity<NhanVien_CongTy>().HasOne<PhongBan>(nv => nv.TenBan)
                                             .WithMany(hd => hd.BanNhanVien_CongTy)
                                             .HasForeignKey(nv => nv.BanId);

            //Add foreign key for NhanVien_CongTy - PhongBan[get TenNhom]
            builder.Entity<NhanVien_CongTy>().HasOne<PhongBan>(nv => nv.TenNhom)
                                             .WithMany(hd => hd.NhomNhanVien_CongTy)
                                             .HasForeignKey(nv => nv.NhomId);

            //Add foreign key for NhanVien - NhanVien [get TenXetDuyet1]
            builder.Entity<NhanVien>().HasOne<NhanVien>(nv => nv.NhanVienXetDuyetCap1)
                                      .WithMany(hd => hd.NhanVienXetDuyetCap1s)
                                      .HasForeignKey(nv => nv.XetDuyetCap1);

            //Add foreign key for NhanVien - NhanVien [get TenXetDuyet2]
            builder.Entity<NhanVien>().HasOne<NhanVien>(nv => nv.NhanVienXetDuyetCap2)
                                      .WithMany(hd => hd.NhanVienXetDuyetCap2s)
                                      .HasForeignKey(nv => nv.XetDuyetCap2);

            //Add foreign key for PhongBan - PhongBan [get parentId]
            builder.Entity<PhongBan>().HasOne<PhongBan>(nv => nv.PhongBanParent)
                                      .WithMany(hd => hd.PhongBanChildren)
                                      .HasForeignKey(nv => nv.Parent);

            //Add foreign key for NhanVien - NhanVien_TinNhan
            builder.Entity<NhanVien_TinNhan>().HasOne<NhanVien>(nvct => nvct.NhanVienNhan)
                                             .WithMany(s => s.NhanVien_TinNhans)
                                             .HasForeignKey(nvct => nvct.NhanVienNhanId);

            //Add foreign key for TinNhan - NhanVien_TinNhan
            builder.Entity<NhanVien_TinNhan>().HasOne<TinNhan>(nvct => nvct.TinNhan)
                                             .WithMany(s => s.NhanVien_TinNhans)
                                             .HasForeignKey(nvct => nvct.TinNhanId);

            //Add foreign key for NhanVien - NhanVien_CMND
            builder.Entity<NhanVien_CMND>().HasOne<NhanVien>(nv => nv.CMNDNhanVien)
                                             .WithMany(s => s.NhanVien_CMNDs)
                                             .HasForeignKey(nvct => nvct.NhanVienId);

            //Add foreign key for TinNhan - NhanVien_CMND
            builder.Entity<NhanVien_CMND>().HasOne<TinhThanh>(nvct => nvct.CMNDNoiCap)
                                             .WithMany(s => s.NhanVien_CMNDNoiCaps)
                                             .HasForeignKey(nvct => nvct.NoiCapId);

            //Add foreign key for NhanVien - BaoHiem
            builder.Entity<NhanVien>().HasOne<BaoHiem>(nv => nv.SoBaoHiem)
                                      .WithOne(s => s.NhanVien)
                                      .HasForeignKey<BaoHiem>(nv => nv.NhanVienId);

            //Add foreign key for Xa - Huyen
            builder.Entity<Xa>().HasOne<Huyen>(xa => xa.Huyen)
                                .WithMany(xa => xa.Xas)
                                .HasForeignKey(nvct => nvct.HuyenId);

            //Add foreign key for Huyen - TinhThanh
            builder.Entity<Huyen>().HasOne<TinhThanh>(hy => hy.TinhThanh)
                                   .WithMany(hy => hy.Huyens)
                                   .HasForeignKey(hy => hy.TinhId);

            //Add foreign key for BaoHiem - Xa
            builder.Entity<BaoHiem>().HasOne<Xa>(bh => bh.BaoHiemXa)
                                     .WithMany(bh => bh.BaoHiems)
                                     .HasForeignKey(hy => hy.XaId);

            //Add foreign key for BaoHiem - Huyen
            builder.Entity<BaoHiem>().HasOne<Huyen>(bh => bh.BaoHiemHuyen)
                                     .WithMany(bh => bh.BaoHiems)
                                     .HasForeignKey(hy => hy.HuyenId);

            //Add foreign key for BaoHiem - TinhThanh
            builder.Entity<BaoHiem>().HasOne<TinhThanh>(bh => bh.BaoHiemTinh)
                                     .WithMany(bh => bh.BaoHiems)
                                     .HasForeignKey(hy => hy.TinhId);

            //Add foreign key Timesheet - NhanVien
            builder.Entity<Timesheet>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.Timesheets)
                                        .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key TongHopDuLieu - NhanVien
            builder.Entity<TongHopDuLieu>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.TongHopDuLieus)
                                        .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key TongHopDuLieu - ViecBenNgoai
            builder.Entity<TongHopDuLieu>().HasOne<ViecBenNgoai>(ts => ts.ViecBenNgoai)
                                        .WithMany(ts => ts.TongHopDuLieus)
                                        .HasForeignKey(ct => ct.ViecBenNgoaiId);

            //Add foreign key TongHopDuLieu - NghiPhep
            builder.Entity<TongHopDuLieu>().HasOne<NghiPhep>(ts => ts.NghiPhep)
                                        .WithMany(ts => ts.TongHopDuLieus)
                                        .HasForeignKey(ct => ct.NghiPhepId);


            //Add foreign key TangCa - NhanVien
            builder.Entity<TangCa>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.TangCas)
                                        .HasForeignKey(nv => nv.NhanVienId);

            //Add foreign key PhuCap - LoaiCongTac
            builder.Entity<PhuCap>().HasOne<LoaiPhuCap>(ts => ts.LoaiPhuCap)
                                        .WithMany(ts => ts.PhuCaps)
                                        .HasForeignKey(ct => ct.LoaiPhuCapId);

            //Add foreign key PhuCap - NhanVien
            builder.Entity<PhuCap>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.PhuCaps)
                                        .HasForeignKey(ct => ct.NhanVienId);

            //Add foreign key NghiPhep - NhanVien
            builder.Entity<NghiPhep>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.NghiPheps)
                                        .HasForeignKey(ct => ct.NhanVienId);

            //Add foreign key CongTac - NhanVien
            builder.Entity<ViecBenNgoai>().HasOne<NhanVien>(ts => ts.NhanVien)
                                        .WithMany(ts => ts.ViecBenNgoais)
                                        .HasForeignKey(ct => ct.NhanVienId);

            //Add foreign key ViecBenNgoai - DiemDen
            builder.Entity<ViecBenNgoai>().HasOne<DiemDen>(ts => ts.DiemDen)
                                        .WithMany(ts => ts.ViecBenNgoais)
                                        .HasForeignKey(ct => ct.DiemDenId);

            //Add foreign key ViecBenNgoai - NhanVien
            builder.Entity<ViecBenNgoai>().HasOne<NhanVien>(ts => ts.NhanVienThayThe)
                                        .WithMany(ts => ts.NhanVienThayThes)
                                        .HasForeignKey(ct => ct.NhanVienThayTheId);

            // add configuration for entity CongTac 
            builder.ApplyConfiguration(new PhuCapConfiguration());

            // add configuration for entity LoaiCongTac 
            builder.ApplyConfiguration(new LoaiPhuCapConfiguration());

            // add configuration for entity NghiPhep 
            builder.ApplyConfiguration(new NghiPhepConfiguration());

            // add configuration for entity CongTac 
            builder.ApplyConfiguration(new ViecBenNgoaiConfiguration());

            // add configuration for entity NghiLe 
            builder.ApplyConfiguration(new NghiLeConfiguration());

            // add configuration for entity CauHinhNgayCong 
            builder.ApplyConfiguration(new CauHinhNgayCongConfiguration());

            // add configuration for entity TongHopDuLieu 
            builder.ApplyConfiguration(new TongHopDuLieuConfiguration());
        }

    }
}
