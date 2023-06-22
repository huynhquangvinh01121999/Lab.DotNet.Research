using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class ChucVu: AuditableBaseEntity
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public int? CapBac { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien> ChucVuNhanViens { get; set; }
        public IList<NhanVien> ChucDanhNhanViens { get; set; }
        public IList<NhanVien_CongTy> ChucVuNhanVien_CongTys { get; set; }
        public IList<NhanVien_CongTy> ChucDanhNhanVien_CongTys { get; set; }

    }
}
