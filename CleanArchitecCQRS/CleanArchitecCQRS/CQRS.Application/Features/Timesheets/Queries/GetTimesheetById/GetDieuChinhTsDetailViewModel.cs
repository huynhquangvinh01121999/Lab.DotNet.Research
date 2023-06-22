using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById
{
    public class GetDieuChinhTsDetailViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? NgayGoiDon { get; set; }
        public string DieuChinh_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_Display { get; set; }
        public string NXD1_Ten { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_Display { get; set; }
        public string NXD2_Ten { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
        public string HR_Display { get; set; }
        public DateTime? HR_GioVao { get; set; }
        public DateTime? HR_GioRa { get; set; }

        public bool? isDisabled { get; set; }

        public int? CurrentStep { get; set; }
    }
}