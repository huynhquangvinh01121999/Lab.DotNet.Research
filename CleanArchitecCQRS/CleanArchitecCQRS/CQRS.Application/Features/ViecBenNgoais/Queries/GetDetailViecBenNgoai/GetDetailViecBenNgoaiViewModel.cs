using System;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai
{
    public class GetDetailViecBenNgoaiViewModel
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string LoaiCongTac { get; set; }
        public string MoTa { get; set; }
        public double? SoGio { get; set; }
        public string TrangThaiXetDuyet { get; set; }
        public string DiaDiem { get; set; }
        public string NguoiGap { get; set; }

        public int DiemDenId { get; set; }
        public string DiemDenTen { get; set; }
        public string DiemDenDiaChi { get; set; }
        public string DiemDenDonVi { get; set; }
        public string DiemDenSdtLienLac { get; set; }
        public string DiemDenNguoiGap { get; set; }
        public string NhanVienThayTheTen { get; set; }

        public string NXD1_Ten { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_Display { get; set; }

        public string NXD2_Ten { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_Display { get; set; }

        public string HR_Ten { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
        public string HR_Display { get; set; }

        public bool? isDisabled { get; set; }
        public bool? isOwner { get; set; }
        public bool? isNXD1 { get; set; }
        public bool? isNXD2 { get; set; }
        public bool? isHR { get; set; }
        public int CurrentStep { get; set; }
    }
}
