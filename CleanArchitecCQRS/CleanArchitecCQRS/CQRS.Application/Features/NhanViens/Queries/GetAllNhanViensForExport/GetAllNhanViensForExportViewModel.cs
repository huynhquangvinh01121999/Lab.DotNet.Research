using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForExport
{
    public class GetAllNhanViensForExportViewModel
    {
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public string GioiTinhTenVN { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string QuocTichTenVN { get; set; }
        public string HonNhanTenVN { get; set; }
        public string TonGiaoTenVN { get; set; }
        public string DanTocTenVN { get; set; }
        public string NguyenQuanTenVN { get; set; }
        public string DiaChiHienTai { get; set; }
        public string NoiSinhTenVN { get; set; }
        public string HoKhau { get; set; }
        public string EmailCaNhan { get; set; }
        public string DienThoaiCaNhan { get; set; }
        public string EmailCongTy { get; set; }
        public string DienThoaiCongTy { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
        public string MaSoThue { get; set; }
        public string SoTaiKhoan { get; set; }
        public string Username { get; set; }
        public string CardNo { get; set; }
        public string NhanVienXetDuyetCap1HoTenDemVN { get; set; }
        public string NhanVienXetDuyetCap1TenVN { get; set; }
        public string NhanVienXetDuyetCap2HoTenDemVN { get; set; }
        public string NhanVienXetDuyetCap2TenVN { get; set; }
        public bool? ChamCongOnline { get; set; }
        public bool? MailNhacNho { get; set; }
        public string TenCongTyTenCongTyVN { get; set; }
        public string TenChucVuTenVN { get; set; }
        public string TenChucDanhTenVN { get; set; }
        public string CapBac { get; set; }
        public string TenPhongTenVN { get; set; }
        public string TenBanTenVN { get; set; }
        public string TenNhomTenVN { get; set; }
        public string CaLamViecTenVN { get; set; }
        public string TrangThaiTenVN { get; set; }
        public double? ThamNien { get; set; }
        public string Avatar { get; set; }
        public string GhiChu { get; set; }
        public string SoBaoHiemSoBHXH { get; set; }
        public string SoBaoHiemSoBHYT { get; set; }
        public string ListGroupMails { get; set; }
        //public string NoiCongTac { get; set; }//
        //public string SoCMND { get; set; }//
    }
}
