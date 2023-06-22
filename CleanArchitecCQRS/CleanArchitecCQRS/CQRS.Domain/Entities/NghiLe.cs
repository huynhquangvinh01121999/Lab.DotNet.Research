using EsuhaiHRM.Domain.Common;
using System;

namespace EsuhaiHRM.Domain.Entities
{
    public class NghiLe : AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime? Ngay { get; set; }
        public DateTime? NgayCoDinh { get; set; }
        public string MoTa { get; set; }
    }
}
