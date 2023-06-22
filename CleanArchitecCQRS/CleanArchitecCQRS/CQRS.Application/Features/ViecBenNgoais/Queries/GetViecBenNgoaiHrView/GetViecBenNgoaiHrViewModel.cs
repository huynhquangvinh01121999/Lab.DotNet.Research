using System;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView
{
    public class GetViecBenNgoaiHrViewModel
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
        public bool NXD1_isHetHanDuyet { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public bool NXD2_isHetHanDuyet { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
