using EsuhaiHRM.Domain.Common;
using System;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_CMND : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public string SoCmnd { get; set; }
        public DateTime? NgayCap { get; set; }
        public int? NoiCapId { get; set; }
        public string UrlFile { get; set; }
        public bool? HieuLuc { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public TinhThanh CMNDNoiCap { get; set; }
        public NhanVien CMNDNhanVien { get; set; }
    }
}
