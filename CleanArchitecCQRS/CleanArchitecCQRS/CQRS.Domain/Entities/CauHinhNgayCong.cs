using EsuhaiHRM.Domain.Common;

namespace EsuhaiHRM.Domain.Entities
{
    public class CauHinhNgayCong : AuditableBaseEntity
    {
        public int Id { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public float? TongNgayCong { get; set; }
        public bool? ChotCong { get; set; }
    }
}
