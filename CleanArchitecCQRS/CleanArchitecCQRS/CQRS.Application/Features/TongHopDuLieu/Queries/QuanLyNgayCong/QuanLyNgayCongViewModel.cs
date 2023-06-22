
using System;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong
{
    public class QuanLyNgayCongViewModel
    {
        public Guid NhanVienId { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenDem { get; set; }
        public string Ten { get; set; }
        public string TenPhong { get; set; }
        public double? SoNgayCong { get; set; }
        public double? SoNgayNghiLe { get; set; }
        public double? SoPhutDiTre { get; set; }
        public double? SoPhutVeSom { get; set; }
        public double? SoGioViecBenNgoai { get; set; }
        public double? SoNgayNghiPhepHopLe { get; set; }
        public double? SoNgayNghiPhepKhongHopLe { get; set; }
        public double? SoNgayCongThucTe { get; set; }
        public double? SoNgayCongThieu { get; set; }
    }
}
