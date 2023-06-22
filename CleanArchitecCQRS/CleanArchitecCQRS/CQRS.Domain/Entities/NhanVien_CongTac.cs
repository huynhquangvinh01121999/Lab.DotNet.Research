using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_CongTac:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public Guid? TrucThuocId { get; set; }
        public Guid? NoiLamViecId { get; set; }
        public Guid? CoSoId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? TinhTrangCongTacId { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public ChiNhanh TrucThuoc { get; set; }
        public ChiNhanh NoiLamViec { get; set; }
        public ChiNhanh CoSo { get; set; }
        public DanhMuc TinhTrangCongTac { get; set; }
    }
}
