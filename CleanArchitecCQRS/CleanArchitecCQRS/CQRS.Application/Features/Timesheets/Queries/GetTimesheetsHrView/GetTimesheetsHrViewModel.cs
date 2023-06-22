using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView
{
    public class GetTimesheetsHrViewModel
    {
        public Guid Id { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string HoTenNhanVienVN { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public DateTime? DieuChinh_GioVao { get; set; }
        public DateTime? DieuChinh_GioRa { get; set; }
        public string DieuChinh_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }

        public DateTime? HR_GioVao { get; set; }
        public DateTime? HR_GioRa { get; set; }

        public DateTime? CaLamViec_BatDau { get; set; }
        public DateTime? CaLamViec_BatDauNghi { get; set; }
        public DateTime? CaLamViec_KetThuc { get; set; }
        public DateTime? CaLamViec_KetThucNghi { get; set; }

        public IList<NghiPhep> NghiPheps { get; set; }
        public IList<ViecBenNgoai> ViecBenNgoais { get; set; }

        public class NghiPhep
        {
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
