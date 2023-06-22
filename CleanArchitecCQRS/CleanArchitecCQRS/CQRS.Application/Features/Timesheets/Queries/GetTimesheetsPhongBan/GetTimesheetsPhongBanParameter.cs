using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan
{
    public class GetTimesheetsPhongBanParameter : RequestParameter
    {
        public DateTime ThoiGian { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string Keyword { get; set; }
    }
}
