using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_VangMat:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string PhanLoai { get; set; }
        public string ThongTin { get; set; }
        public string GhiChu { get; set; }
        public bool? TinhThamNien { get; set; }
        public NhanVien NhanViens { get; set; }
    }
}
