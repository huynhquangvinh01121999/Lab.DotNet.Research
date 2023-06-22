using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_BangCapChungChi : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? PhanLoaiId { get; set; }
        public string PhanLoaiKhac { get; set; }
        public string ChungChi { get; set; }
        public DateTime? NgayCap { get; set; }
        public string TrinhDoThucTe { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public DanhMuc LoaiBangCapChungChi { get; set; }
    }
}
