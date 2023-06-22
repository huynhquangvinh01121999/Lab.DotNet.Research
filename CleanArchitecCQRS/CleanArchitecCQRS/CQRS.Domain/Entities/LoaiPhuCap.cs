using EsuhaiHRM.Domain.Common;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class LoaiPhuCap : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Ten { get; set; }
        public string TenJP { get; set; }
        public float? SoTien { get; set; }
        public string MoTa { get; set; }
        public IList<PhuCap> PhuCaps { get; set; }
    }
}
