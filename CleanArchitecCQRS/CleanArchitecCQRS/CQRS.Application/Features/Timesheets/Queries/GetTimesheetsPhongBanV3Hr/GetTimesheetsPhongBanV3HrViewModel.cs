using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr
{
    public class GetTimesheetsPhongBanV3HrViewModel
    {
        public Guid? NhanVienId { get; set; }
        public string HoTen { get; set; }
        public string MaNhanVien { get; set; }
        public string PhongBanTen { get; set; }
        public IList<DanhSachNgayCong> thdl { get; set; }

        public class DanhSachNgayCong
        {
            public DateTime NgayLamViec { get; set; }
            public float? SoNgayCong { get; set; }
            public string Thu { get; set; }
            public IList<DanhSachPhanLoai> List { get; set; }

            public class DanhSachPhanLoai
            {
                public string PhanLoai { get; set; }
                public DateTime? ThoiGianBatDau { get; set; }
                public DateTime? ThoiGianKetThuc { get; set; }
                public string TrangThai { get; set; }
            }
        }
    }
}
