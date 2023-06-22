using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class TinhThanh:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string MaTinh { get; set; }
        public string TenTinhVN { get; set; }
        public string TenTinhEN { get; set; }
        public string TenTinhJP { get; set; }
        public string VungMienVN { get; set; }
        public string VungMienJP { get; set; }
        public string PhanLoaiVN { get; set; }
        public string PhanLoaiEN { get; set; }
        public string PhanLoaiJP { get; set; }
        public IList<BenhVienBHXH> BenhVienBHXHs { get; set; }
        public IList<ChiNhanh> ChiNhanhs { get; set; }
        public IList<NhanVien_CMND> NhanVien_CMNDNoiCaps { get; set; }
        public IList<Huyen> Huyens { get; set; }
        public IList<BaoHiem> BaoHiems { get; set; }
    }
}
