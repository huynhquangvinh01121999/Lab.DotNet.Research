using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView
{
    public class GetTangCasNotHrViewModel
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime Created { get; set; }
        public string Thu { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }

        public DateTime? Timesheet_GioVao { get; set; }
        public DateTime? Timesheet_GioRa { get; set; }
        public float? DiTre { get; set; }
        public float? VeSom { get; set; }

        public string HoTenNhanVienVN { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }

        public DateTime NgayTangCa { get; set; }
        public string TrangThai { get; set; }
        public float? SoGioDangKy { get; set; }
        public float? SoGioNgayLe { get; set; }
        public float? SoGioCuoiTuan { get; set; }
        public float? SoGioNgayThuong { get; set; }
        public float? SoGioDoiChieu { get; set; }
        public float? SoGioDuocDuyet { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }
        public bool? IsXetDuyetCap1 { get; set; }
        public bool? NXD1_isHetHanDuyet { get; set; }
        public string NXD1_Ten { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public bool? IsXetDuyetCap2 { get; set; }
        public bool? NXD2_isHetHanDuyet { get; set; }
        public string NXD2_Ten { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }

        public IList<NghiPhep> NghiPheps { get; set; }
        public IList<ViecBenNgoai> ViecBenNgoais { get; set; }

        public class NghiPhep
        {
            public Guid? Id { get; set; }
            public DateTime? ThoiGianBatDau { get; set; }
            public DateTime? ThoiGianKetThuc { get; set; }
            public float? SoNgayDangKy { get; set; }
            public string TrangThaiNghi { get; set; }
            public string TrangThaiDangKy { get; set; }
            public string MoTa { get; set; }
            public DateTime? TGBD_Display { get; set; }
            public DateTime? TGKT_Display { get; set; }
        }

        public class ViecBenNgoai
        {
            public Guid? Id { get; set; }
            public DateTime? ThoiGianBatDau { get; set; }
            public DateTime? ThoiGianKetThuc { get; set; }
            public DateTime? TGBD_Display { get; set; }
            public DateTime? TGKT_Display { get; set; }
            public float? SoGio { get; set; }
            public string MoTa { get; set; }
            public string LoaiCongTac { get; set; }
        }
    }
}
