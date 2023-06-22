using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_BaoHiem:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public decimal? LuongCoBan { get; set; }
        public decimal? PhuCapChucVu { get; set; }
        public decimal? PhuCapThuHut { get; set; }
        public decimal? TongPhuCap { get; set; }
        public decimal? LuongBHXH { get; set; }
        public int? BenhVienId { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public BenhVienBHXH BenhVienBHXH { get; set; }
    }
}
