using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_DanhGia:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? LoaiDanhGiaId { get; set; }
        public string XepLoai { get; set; }
        public DateTime? NgayDanhGia { get; set; }
        public string MucTieu { get; set; }
        public string NhanXet { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc LoaiDanhGia { get; set; }
    }
}
