using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView
{
    public class GetTimesheetsHrViewParameter : RequestParameter
    {
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
