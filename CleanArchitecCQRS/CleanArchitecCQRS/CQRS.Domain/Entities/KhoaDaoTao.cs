using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class KhoaDaoTao:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime? NgayDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiDaoTao { get; set; }
        public string MucDich { get; set; }
        public string DiaDiem { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien_KhoaDaoTao> NhanVien_KhoaDaoTaos { get; set; }

    }
}
