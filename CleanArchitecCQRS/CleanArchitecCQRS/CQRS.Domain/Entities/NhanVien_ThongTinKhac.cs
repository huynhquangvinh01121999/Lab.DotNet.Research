using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_ThongTinKhac : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public string PhanLoaiKhac { get; set; }
        public int? PhanLoaiId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string ThongTin1 { get; set; }
        public string ThongTin2 { get; set; }
        public string ThongTin3 { get; set; }
        public bool? CQPCEsuhai { get; set; }
        public int? HocVienId { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc LoaiXuatCanh { get; set; }
    }
}
