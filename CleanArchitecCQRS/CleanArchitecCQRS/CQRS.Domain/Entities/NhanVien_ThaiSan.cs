using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_ThaiSan:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string TroCap { get; set; }
        public DateTime? NgaySinhCon { get; set; }
        public DateTime? NgayHetCheDoChamCon { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
