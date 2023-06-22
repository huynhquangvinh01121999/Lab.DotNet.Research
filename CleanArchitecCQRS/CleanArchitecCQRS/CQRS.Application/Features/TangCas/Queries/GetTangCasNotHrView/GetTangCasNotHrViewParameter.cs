using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView
{
    public class GetTangCasNotHrViewParameter : RequestParameter
    {
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; } = "all";
        public string Keyword { get; set; } = " ";
    }
}
