using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class CongTy: AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTyVN { get; set; }
        public string TenCongTyEN { get; set; }
        public string TenCongTyJP { get; set; }
        public string TenVietTat { get; set; }
        public string TenGiamDoc { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien> NhanViens { get; set; }
        public IList<NhanVien_CongTy> NhanVien_CongTys { get; set; }
        public IList<NhanVien_HopDong> NhanVien_HopDongs { get; set; }
    }
}
