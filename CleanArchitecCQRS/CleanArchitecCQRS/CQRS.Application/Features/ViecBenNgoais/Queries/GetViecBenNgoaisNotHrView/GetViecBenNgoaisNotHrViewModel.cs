using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView
{
    public class GetViecBenNgoaisNotHrViewModel
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

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public bool? IsXetDuyetCap1 { get; set; }
        public bool NXD1_isHetHanDuyet { get; set; }
        public string NXD1_Ten { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public bool? IsXetDuyetCap2 { get; set; }
        public bool NXD2_isHetHanDuyet { get; set; }
        public string NXD2_Ten { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
