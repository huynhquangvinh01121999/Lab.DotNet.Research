using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_TinNhan : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public Guid NhanVienNhanId { get; set; }
        public Guid TinNhanId { get; set; }
        public bool? isRead { get; set; }
        public bool? isReadUrl { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVienNhan { get; set; }
        public TinNhan TinNhan { get; set; }
    }
}
