using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class TinNhan : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public Guid? NhanVienGuiId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NhanHieu { get; set; }
        public string UrlThongTin { get; set; }
        public DateTime NgayGui { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien_TinNhan> NhanVien_TinNhans { get; set; }
    }
}
