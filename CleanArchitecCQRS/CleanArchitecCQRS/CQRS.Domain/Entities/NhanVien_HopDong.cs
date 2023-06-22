using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_HopDong:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? CongTyId { get; set; }
        public int? LoaiHopDongId { get; set; } 
        public string SoHopDong { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public DateTime? NgayKy { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public CongTy CongTy { get; set; }
        public DanhMuc LoaiHopDong { get; set; }
    }
}
