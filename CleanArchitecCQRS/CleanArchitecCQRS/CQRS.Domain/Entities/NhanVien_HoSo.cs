using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_HoSo : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? HoSoId { get; set; }
        public DateTime? NgayNhan { get; set; }
        public DateTime? HanBoSung { get; set; }
        public int? LoaiHoSoId { get; set; } 
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc TenHoSo { get; set; }
        public DanhMuc TenLoaiHoSo { get; set; }
    }
}
