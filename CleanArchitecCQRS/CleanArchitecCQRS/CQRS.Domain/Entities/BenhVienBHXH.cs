using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class BenhVienBHXH : AuditableBaseEntity
    {
        public int Id { get; set; }
        public int TinhId { get; set; }
        public string TenBenhVien { get; set; }
        public string MaBenhVien { get; set; }
        public TinhThanh TinhThanh { get; set; }
        public IList<NhanVien_BaoHiem> NhanVien_BaoHiems { get; set; }
    }
}
