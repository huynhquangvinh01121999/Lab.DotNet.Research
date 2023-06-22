using EsuhaiHRM.Domain.Common;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class DiemDen : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string DonVi { get; set; }
        public string SdtLienLac { get; set; }
        public string NguoiGap { get; set; }
        public string ChucDanhNguoiGap { get; set; }

        public IList<ViecBenNgoai> ViecBenNgoais { get; set; }
    }
}
