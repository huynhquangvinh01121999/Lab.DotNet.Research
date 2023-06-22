using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_CongTy:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? CongTyId { get; set; }
        public int? ChucVuId { get; set; }
        public int? ChucDanhId { get; set; }
        public string CapBac { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public int? NhomId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TinhTrang { get; set; }
        public bool? IsCongTyChinh { get; set; }
        public string GhiChu { get; set; }
        public CongTy CongTy { get; set; }
        public NhanVien NhanVien { get; set; }
        public ChucVu ChucVu { get; set; }
        public ChucVu ChucDanh { get; set; }
        public PhongBan TenPhong { get; set; }
        public PhongBan TenBan { get; set; }
        public PhongBan TenNhom { get; set; }

    }
}
