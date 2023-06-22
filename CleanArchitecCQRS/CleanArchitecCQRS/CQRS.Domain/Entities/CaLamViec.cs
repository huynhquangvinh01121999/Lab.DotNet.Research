using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class CaLamViec: AuditableBaseEntity
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public DateTime BatDauNghi { get; set; }
        public DateTime KetThucNghi { get; set; }
        public bool? KhacNgay { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien> NhanViens { get; set; }
        public IList<NhanVien_CaLamViec> NhanVien_CaLamViecs { get; set; }
    }
}
