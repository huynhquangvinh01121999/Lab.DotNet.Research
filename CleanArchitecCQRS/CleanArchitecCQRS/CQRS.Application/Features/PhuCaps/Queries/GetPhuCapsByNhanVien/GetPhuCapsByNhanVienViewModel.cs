using System;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien
{
    public class GetPhuCapsByNhanVienViewModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid NhanVienId { get; set; }
        public string PhanLoai { get; set; }
        public string PhanLoaiJP { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public int? LoaiPhuCapId { get; set; }

        public short? SoLanPhuCap { get; set; }
        public short? SoBuoiSang { get; set; }
        public short? SoBuoiTrua { get; set; }
        public short? SoBuoiChieu { get; set; }
        public short? SoQuaDem { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public DateTime? XD_ThoiGianBatDau { get; set; }
        public DateTime? XD_ThoiGianKetThuc { get; set; }
        public short? XD_SoLanPhuCap { get; set; }
        public short? XD_SoBuoiSang { get; set; }
        public short? XD_SoBuoiTrua { get; set; }
        public short? XD_SoBuoiChieu { get; set; }
        public short? XD_SoQuaDem { get; set; }

        public short? SoLanCuoiTuan { get; set; }
        public short? SoLanNgayLe { get; set; }
        public short? SoLanNgayThuong { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_GhiChu { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_TrangThai { get; set; }


        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_GhiChu { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_TrangThai { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_GhiChu { get; set; }
        public string HR_TrangThai { get; set; }

        public bool? isDisabled { get; set; }
    }
}
