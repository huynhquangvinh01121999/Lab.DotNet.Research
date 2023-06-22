using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuNhanVien
{
    public class GetTongHopDuLieuNhanVienVModel
    {
        public float? TongGioCong { get; set; }
        public float? TongNgayCong { get; set; }
        public float? TongDiTre { get; set; }
        public float? TongVeSom { get; set; }
        public float? TongNghiPhepHopLe { get; set; }
        public float? TongNghiPhepKhongHopLe { get; set; }
        public float? TongViecBenNgoaiCaNhan { get; set; }
        public float? TongViecBenNgoaiCongTy { get; set; }
        public bool? ChamCongOnline { get; set; }

        public IList<TimesheetNgayCong> Timesheets { get; set; }

        public class TimesheetNgayCong
        {
            public int? Id { get; set; }
            public string HoTenNhanVien { get; set; }
            public Guid? NhanVienId { get; set; }
            public DateTime? NgayLamViec { get; set; }
            public DateTime? CaLamViec_BatDau { get; set; }
            public DateTime? CaLamViec_BatDauNghi { get; set; }
            public DateTime? CaLamViec_KetThucNghi { get; set; }
            public DateTime? CaLamViec_KetThuc { get; set; }
            public string Timesheet_GioVao { get; set; }
            public string Timesheet_GioRa { get; set; }
            public float? NgayCong { get; set; }
            public float? Final_GioCong { get; set; }
            public float? DiTre { get; set; }
            public float? VeSom { get; set; }
            public int? SoNgay { get; set; }

            public string DieuChinh_GioVao { get; set; }
            public string DieuChinh_GioRa { get; set; }
            public string DieuChinh_GhiChu { get; set; }

            public string TrangThai { get; set; }
            public Guid? TimesheetId { get; set; }
            public string TimesheetTrangThai { get; set; }

            public string HR_TrangThai { get; set; }
            public DateTime? HR_GioVao { get; set; }
            public DateTime? HR_GioRa { get; set; }

            public IList<TangCaInfo> TangCas { get; set; }
            public IList<PhuCapInfo> PhuCaps { get; set; }
            public IList<NghiPhep> NghiPheps { get; set; }
            public IList<ViecBenNgoai> ViecBenNgoais { get; set; }

            public class TangCaInfo
            {
                public DateTime? ThoiGianBatDau { get; set; }
                public DateTime? ThoiGianKetThuc { get; set; }
                public float? SoGioDangKy { get; set; }
                public string MoTa { get; set; }
            }

            public class PhuCapInfo
            {
                public DateTime? ThoiGianBatDau { get; set; }
                public DateTime? ThoiGianKetThuc { get; set; }
                public string LoaiPhuCapTen { get; set; }
                public string LoaiPhuCapTenJP { get; set; }
            }

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
}
