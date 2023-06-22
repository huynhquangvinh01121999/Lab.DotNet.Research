using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class PhongBan:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public Guid? TruongBoPhanId { get; set; }
        public int? Parent { get; set; }
        public int? GroupMailId { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public NhanVien TruongBoPhan { get; set; }
        public GroupMail GroupMail { get; set; }
        public IList<NhanVien> NhanVienPhongs { get; set; }
        public IList<NhanVien> NhanVienBans { get; set; }
        public IList<NhanVien> NhanVienNhoms { get; set; }
        public IList<NhanVien_CongTy> PhongNhanVien_CongTy { get; set; }
        public IList<NhanVien_CongTy> BanNhanVien_CongTy { get; set; }
        public IList<NhanVien_CongTy> NhomNhanVien_CongTy { get; set; }
        public PhongBan PhongBanParent { get; set; }
        public IList<PhongBan> PhongBanChildren { get; set; }

    }
}
