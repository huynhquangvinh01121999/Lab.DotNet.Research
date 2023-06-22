using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_ThongTinGiaDinh:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public string HoTenVN { get; set; }
        public int? QuanHeId { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc QuanHe { get; set; }
    }
}
