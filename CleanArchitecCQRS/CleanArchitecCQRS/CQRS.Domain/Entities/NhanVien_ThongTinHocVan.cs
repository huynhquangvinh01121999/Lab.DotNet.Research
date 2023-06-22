using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_ThongTinHocVan : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? TrinhDoId { get; set; }
        public string TenTruongVN { get; set; }
        public int? NhomNganhId { get; set; }
        public string ChuyenNganh { get; set; }
        public bool? IsCaoNhat { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc TrinhDo { get; set; }
        public DanhMuc NhomNganh { get; set; }
    }
}
