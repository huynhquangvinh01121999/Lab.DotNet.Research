using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_ThoiViec:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public DateTime? NgayNopDon { get; set; }
        public DateTime? NgayThoiViec { get; set; }
        public string NguyenNhan { get; set; }
        public bool? DungLuatLD { get; set; }
        public bool? DungQuyCheCongTy { get; set; }
        public DateTime? NgayKy { get; set; }
        public string SoQuyetDinh { get; set; }
        public string TroCap { get; set; }
        public string GhiChu { get; set; }
        public bool? TinhThamNien { get; set; }
        public DateTime? NgayQuayLai { get; set; }
        public NhanVien NhanVien { get; set; }

    }
}
