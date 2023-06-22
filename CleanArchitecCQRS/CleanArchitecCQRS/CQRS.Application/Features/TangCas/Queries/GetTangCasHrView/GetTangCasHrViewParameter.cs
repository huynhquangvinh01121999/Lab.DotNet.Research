using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView
{
    public class GetTangCasHrViewParameter : RequestParameter
    {
        public int PhongId { get; set; } = 0;
        public int BanId { get; set; } = 0;
        public string TrangThai { get; set; } = "all";
        public string Keyword { get; set; } = " ";
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
