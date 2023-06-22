using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetDetailNghiPhep
{
    public class GetDetailNghiPhepViewModel
    {
        public string HoTen { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public double? SoNgayDangKy { get; set; }
        public string MoTa { get; set; }
        public string TrangThaiDangKy { get; set; }
        public string TrangThaiNghi { get; set; }
        public string CongViecThayThe { get; set; }
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
