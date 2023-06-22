using System;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCaps
{
    public class GetPhuCapsViewModel
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public int LoaiPhuCapId { get; set; }

        public string HoTenNhanVienVN { get; set; }
        public string LoaiPhuCapTen { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public int? SoLanPhuCap { get; set; }
        public int? SoBuoiSang { get; set; }
        public int? SoBuoiChieu { get; set; }
        public int? SoBuoiTrua { get; set; }
        public int? SoQuaDem { get; set; }

        public string MoTa { get; set; }

        public DateTime? XD_ThoiGianBatDau { get; set; }
        public DateTime? XD_ThoiGianKetThuc { get; set; }

        public int? XD_SoLanPhuCap { get; set; }
        public int? XD_SoBuoiSang { get; set; }
        public int? XD_SoBuoiChieu { get; set; }
        public int? XD_SoBuoiTrua { get; set; }
        public int? XD_SoQuaDem { get; set; }

        public int? SoLanCuoiTuan { get; set; }
        public int? SoLanNgayLe { get; set; }
        public int? SoLanNgayThuong { get; set; }

        public string TrangThai { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
