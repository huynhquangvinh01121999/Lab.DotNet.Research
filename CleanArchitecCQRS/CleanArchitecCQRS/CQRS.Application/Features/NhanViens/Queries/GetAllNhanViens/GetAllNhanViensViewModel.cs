using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensViewModel
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public int? GioiTinhId { get; set; }
        public DateTime NgaySinh { get; set; }
        public int? ChucVuId { get; set; }
        public int? ChucDanhId { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
        public double? ThamNien { get; set; }
        public string Avatar { get; set; }
        public string GhiChu { get; set; }
        public string AccountId { get; set; }

        public string GioiTinhTenVN { get; set; }
        public string GioiTinhTenJP { get; set; }
        public string TenChucVuTenVN { get; set; }
        public string TenChucVuTenJP { get; set; }
        public string TenChucDanhTenVN { get; set; }
        public string TenChucDanhTenJP { get; set; }
        public string TenPhongTenVN { get; set; }
        public string TenPhongTenJP { get; set; }
        public string TenBanTenVN { get; set; }
        public string TenBanTenJP { get; set; }
        public string TenNhomTenVN { get; set; }
        public string TenNhomTenJP { get; set; }
        public string TrangThaiTenVN { get; set; }
        //public string QuocTich { get; set; }
        //public int? HonNhanId { get; set; }
        //public int? TonGiaoId { get; set; }
        //public int? DanTocId { get; set; }
        //public int? NguyenQuanId { get; set; }
        //public string DiaChiHienTai { get; set; }
        //public int? NoiSinhId { get; set; }
        //public string HoKhau { get; set; }
        //public string EmailCaNhan { get; set; }
        //public string DienThoaiCaNhan { get; set; }
        //public string EmailCongTy { get; set; }
        //public string DienThoaiCongTy { get; set; }
        //public string MaSoThue { get; set; }
        //public string SoTaiKhoan { get; set; }
        //public string CMND_So { get; set; }
        //public DateTime? CMND_NgayCap { get; set; }
        //public int? CMND_NoiCapId { get; set; }
        //public string Passport_So { get; set; }
        //public DateTime? Passport_NgayCap { get; set; }
        //public int? Passport_NoiCapId { get; set; }
        //public string Username { get; set; }
        //public string CardNo { get; set; }
        //public int? XetDuyetCap1 { get; set; }
        //public int? XetDuyetCap2 { get; set; }
        //public bool? ChamCongOnline { get; set; }
        //public string MailNhacNho { get; set; }
        //public int? CongTyId { get; set; }
        //public string CapBac { get; set; }
        //public int? NhomId { get; set; }
        //public int? CaLamViecId { get; set; }
        //public string TenCongTyTenCongTyVN { get; set; }
        //public string HonNhanTenVN { get; set; }
        //public string GioiTinhTenVN { get; set; }
        //public string TonGiaoTenVN { get; set; }
        //public string NguyenQuanTenVN { get; set; }
        //public string NoiSinhTenVN { get; set; }
        //public string CMND_NoiCapTenTinhVN { get; set; }
        //public string Passport_NoiCapTenTinhVN { get; set; }

    }
}
