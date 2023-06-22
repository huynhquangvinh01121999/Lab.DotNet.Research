using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien
{
    public class GetTimesheetsByNhanVienViewModel
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayLamViec { get; set; }
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
        public DateTime? NgayGoiDon { get; set; }
        public string TrangThai { get; set; }
    }
}