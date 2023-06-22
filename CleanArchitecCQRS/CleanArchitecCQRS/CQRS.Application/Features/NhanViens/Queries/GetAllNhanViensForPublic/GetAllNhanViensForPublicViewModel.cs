using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic
{
    public class GetAllNhanViensForPublicViewModel
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public int? GioiTinhId { get; set; }
        public DateTime NgaySinh { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public int? ChucVuId { get; set; }
        public string DienThoaiCaNhan { get; set; }
        public string EmailCaNhan { get; set; }
        public string DienThoaiCongTy { get; set; }
        public string EmailCongTy { get; set; }
        public string SoTaiKhoan { get; set; }
        public string HoKhau { get; set; }
        public string DiaChiHienTai { get; set; }
        public int? QuocTichId { get; set; }
        public int? DanTocId { get; set; }
        public int? HonNhanId { get; set; }
        public string Avatar { get; set; }
        public string GioiTinhTenVN { get; set; }
        public string GioiTinhTenJP { get; set; }
        public string TenPhongTenVN { get; set; }
        public string TenPhongTenJP { get; set; }
        public string TenBanTenVN { get; set; }
        public string TenBanTenJP { get; set; }
        public string TenChucVuTenVN { get; set; }
        public string TenChucVuTenJP { get; set; }
        public string QuocTichTenVN { get; set; }
        public string QuocTichTenJP { get; set; }
        public string DanTocTenVN { get; set; }
        public string DanTocTenJP { get; set; }
        public string HonNhanTenVN { get; set; }
        public string HonNhanTenJP { get; set; }
        public string GroupMails { get; set; }
    }
}
