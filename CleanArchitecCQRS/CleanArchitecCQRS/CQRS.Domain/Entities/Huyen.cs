using EsuhaiHRM.Domain.Common;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class Huyen:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string MaHuyen { get; set; }
        public string TenHuyenVN { get; set; }
        public string TenHuyenEN { get; set; }
        public string PhanLoaiHuyenVN { get; set; }
        public string PhanLoaiHuyenEN { get; set; }
        public int? TinhId { get; set; }
        public TinhThanh TinhThanh { get; set; }
        public IList<Xa> Xas { get; set; }
        public IList<BaoHiem> BaoHiems { get; set; }
    }
}
