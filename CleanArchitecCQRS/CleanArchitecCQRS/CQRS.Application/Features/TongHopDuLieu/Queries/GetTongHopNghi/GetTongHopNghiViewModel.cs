using System;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi
{
    public class GetTongHopNghiViewModel
    {
        public string HoTen { get; set; }
        public string MaNhanVien { get; set; }
        public string StrNgayLamViec { get; set; }

        public double? NgayCongDiLam { get; set; }
        public double? TongNgayNghiPhep { get; set; }
        public double? TongNgayNghiLe { get; set; }
        public double? ViecRieng { get; set; }
        public double? TongNghi { get; set; }
        public double? NgayCongTinhLuong { get; set; }

    }
}
