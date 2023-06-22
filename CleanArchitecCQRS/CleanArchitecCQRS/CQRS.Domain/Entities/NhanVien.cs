using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public int? GioiTinhId { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? QuocTichId { get; set; }
        public int? HonNhanId { get; set; }
        public int? TonGiaoId { get; set; }
        public int? DanTocId { get; set; }
        public int? NguyenQuanId { get; set; }
        public string DiaChiHienTai { get; set; }
        public int? NoiSinhId { get; set; }
        public string HoKhau { get; set; }
        public string EmailCaNhan { get; set; }
        public string DienThoaiCaNhan { get; set; }
        public string EmailCongTy { get; set; }
        public string DienThoaiCongTy { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
        public string MaSoThue { get; set; }
        public string SoTaiKhoan { get; set; }
        //public string CMND_So { get; set; }
        //public DateTime? CMND_NgayCap { get; set; }
        //public string Passport_So { get; set; }
        //public DateTime? Passport_NgayCap { get; set; }
        //public int? Passport_NoiCapId { get; set; }
        public string Username { get; set; }
        public string CardNo { get; set; }
        public Guid? XetDuyetCap1 { get; set; }
        public Guid? XetDuyetCap2 { get; set; }
        public bool? ChamCongOnline { get; set; }
        public bool? MailNhacNho { get; set; }
        public int? CongTyId { get; set; }
        public int? ChucVuId { get; set; }
        public int? ChucDanhId { get; set; }
        public string CapBac { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public int? NhomId { get; set; }
        public int? CaLamViecId { get; set; }
        public int? TrangThaiId { get; set; } // tập sự, thử việc, chính thức, thai sản, tạm nghĩ, thôi việc
        public double? ThamNien { get; set; }
        public string Avatar { get; set; }
        public string GhiChu { get; set; }
        public string AccountId { get; set; }
        public CongTy TenCongTy { get; set; }
        public ChucVu TenChucVu { get; set; }
        public ChucVu TenChucDanh { get; set; }
        public BaoHiem SoBaoHiem { get; set; }
        public CaLamViec CaLamViec { get; set; }
        public IList<NhanVien_CongTy> NhanVien_CongTys { get; set; }
        public IList<NhanVien_GroupMail> NhanVien_GroupMails { get; set; }
        public IList<NhanVien_KhoaDaoTao> NhanVien_KhoaDaoTaos { get; set; }
        public IList<NhanVien_VangMat> NhanVien_VangMats { get; set; }
        public IList<NhanVien_ThongTinGiaDinh> NhanVien_ThongTinGiaDinhs { get; set; }
        public IList<NhanVien_ThongTinHocVan> NhanVien_ThongTinHocVans { get; set; }
        public IList<NhanVien_BangCapChungChi> NhanVien_BangCapChungChis { get; set; }
        public IList<NhanVien_BaoHiem> NhanVien_BaoHiems { get; set; }
        public IList<NhanVien_CaLamViec> NhanVien_CaLamViecs { get; set; }
        public IList<NhanVien_DanhGia> NhanVien_DanhGias { get; set; }
        public IList<NhanVien_GiayPhep> NhanVien_GiayPheps { get; set; }
        public IList<NhanVien_HopDong> NhanVien_HopDongs { get; set; }
        public IList<NhanVien_HoSo> NhanVien_HoSos { get; set; }
        public IList<NhanVien_ThongTinKhac> NhanVien_ThongTinKhacs { get; set; }
        public IList<NhanVien_PhuCap> NhanVien_PhuCaps { get; set; }
        public IList<NhanVien_ThaiSan> NhanVien_ThaiSans { get; set; }
        public IList<NhanVien_ThoiViec> NhanVien_ThoiViecs { get; set; }
        public IList<NhanVien_CongTac> NhanVien_CongTacs { get; set; }
        public IList<NhanVien_TinNhan> NhanVien_TinNhans { get; set; }
        public IList<NhanVien_CMND> NhanVien_CMNDs { get; set; }
        public IList<PhongBan> TruongBoPhans { get; set; }
        public IList<GroupMail> DeNghiGroupMails { get; set; }
        public NhanVien NhanVienXetDuyetCap1 { get; set; }
        public NhanVien NhanVienXetDuyetCap2 { get; set; }
        public IList<NhanVien> NhanVienXetDuyetCap1s { get; set; }
        public IList<NhanVien> NhanVienXetDuyetCap2s { get; set; }
        public DanhMuc GioiTinh { get; set; }
        public DanhMuc HonNhan { get; set; }
        public DanhMuc DanToc { get; set; }
        public DanhMuc TonGiao { get; set; }
        public DanhMuc NguyenQuan { get; set; }
        public DanhMuc NoiSinh { get; set; }
        public DanhMuc QuocTich { get; set; }
        public PhongBan TenPhong { get; set; }
        public PhongBan TenBan { get; set; }
        public PhongBan TenNhom { get; set; }
        public DanhMuc TrangThai { get; set; }
        public IList<Timesheet> Timesheets { get; set; }
        public IList<TongHopDuLieu> TongHopDuLieus { get; set; }
        public IList<TangCa> TangCas { get; set; }
        public IList<PhuCap> PhuCaps { get; set; }
        public IList<NghiPhep> NghiPheps { get; set; }
        public IList<ViecBenNgoai> ViecBenNgoais { get; set; }
        public IList<ViecBenNgoai> NhanVienThayThes { get; set; }
    }
}
