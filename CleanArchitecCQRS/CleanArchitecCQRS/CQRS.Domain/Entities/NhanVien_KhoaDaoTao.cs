using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_KhoaDaoTao:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int KhoaDaoTaoId { get; set; }
        public bool? ChungChi { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public KhoaDaoTao KhoaDaoTao { get; set; }

    }
}
