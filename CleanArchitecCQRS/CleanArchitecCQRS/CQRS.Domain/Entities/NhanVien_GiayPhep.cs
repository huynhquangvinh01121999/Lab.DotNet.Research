using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_GiayPhep:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public string SoGiayPhep { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string ViTriCongViec { get; set; }
        public string ChucDanh { get; set; }
        public DateTime? NgayNhac { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
