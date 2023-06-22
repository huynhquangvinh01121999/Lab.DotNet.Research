using EsuhaiHRM.Domain.Common;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class Xa:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string MaXa { get; set; }
        public string TenXaVN { get; set; }
        public string TenXaEN { get; set; }
        public string PhanLoaiXaVN { get; set; }
        public string PhanLoaiXaEN { get; set; }
        public int? HuyenId { get; set; }
        public Huyen Huyen { get; set; }
        public IList<BaoHiem> BaoHiems { get; set; }
    }
}
