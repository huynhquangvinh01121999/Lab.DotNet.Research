using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class ChiNhanh : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public int? LoaiChiNhanhId { get; set; }
        public Guid? Parent { get; set; }
        public string DiaChi { get; set; }
        public int? TinhId { get; set; }
        public bool? TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DanhMuc LoaiChiNhanh { get; set; }
        public TinhThanh TinhThanh { get; set; }
        public IList<NhanVien_CongTac> NhanVien_TrucThuocs { get; set; }
        public IList<NhanVien_CongTac> NhanVien_NoiLamViecs { get; set; }
        public IList<NhanVien_CongTac> NhanVien_CoSos { get; set; }
        public ChiNhanh TruSoChinh { get; set; }
        public IList<ChiNhanh> ChiNhanhNhos { get; set; }

    }
}
