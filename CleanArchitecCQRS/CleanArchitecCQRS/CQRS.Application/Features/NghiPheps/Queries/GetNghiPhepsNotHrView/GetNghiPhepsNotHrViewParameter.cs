using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView
{
    public class GetNghiPhepsNotHrViewParameter : RequestParameter
    {
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }
}
