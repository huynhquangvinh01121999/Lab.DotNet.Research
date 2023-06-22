using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien
{
    public class GetTongHopDuLieusByNhanVienViewModel
    {
        public int Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayLamViec { get; set; }
        public bool? isNgayLe { get; set; }
        public bool? isCuoiTuan { get; set; }

        public DateTime? Timesheet_GioVao { get; set; }
        public DateTime? Timesheet_GioRa { get; set; }
        public float? Timesheet_NgayCong { get; set; }
        public float? Timesheet_GioCong { get; set; }

        public float? DiTre { get; set; }
        public float? VeSom { get; set; }

        public float? NgayCong { get; set; }
        public float? Final_GioCong { get; set; }

        public DateTime? Final_GioVao { get; set; }
        public DateTime? Final_GioRa { get; set; }

        public Guid? TsId { get; set; }

        public Guid? TsNxd1 { get; set; }
        public string TsNxd1TrangThai { get; set; }
        public string TsNxd1GhiChu { get; set; }

        public Guid? TsNxd2 { get; set; }
        public string TsNxd2TrangThai { get; set; }
        public string TsNxd2GhiChu { get; set; }

        public Guid? TsHRxd { get; set; }
        public string TsHRxdTrangThai { get; set; }
        public string TsHRxdGhiChu { get; set; }

        public DateTime? TsDieuChinhGioVao { get; set; }
        public DateTime? TsDieuChinhGioRa { get; set; }
        public string TsDieuChinhGhiChu { get; set; }
        public string TsTrangThai { get; set; }

        public int? DaysToNow { get; set; }
        public double? SoGioTangCa { get; set; }

    }
}