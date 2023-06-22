using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetAllChiNhanhs
{
    public class GetAllChiNhanhsViewModel
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
        public string LoaiChiNhanhTenVN { get; set; }
        public string LoaiChiNhanhTenJP { get; set; }
        public string TinhThanhTenTinhVN { get; set; }
        public string TinhThanhTenTinhJP { get; set; }
        public string TruSoChinhTenVN { get; set; }
        public string TruSoChinhTenJP { get; set; }

    }
}
