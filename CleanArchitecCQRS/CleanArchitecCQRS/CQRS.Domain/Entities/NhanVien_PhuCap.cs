using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_PhuCap:AuditableBaseEntity
    {   
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? LoaiPhuCapId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public decimal? GiaTri { get; set; }
        public string GhiChu { get; set; }
        public bool? HieuLuc { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc LoaiPhuCap { get; set; }
    }
}
