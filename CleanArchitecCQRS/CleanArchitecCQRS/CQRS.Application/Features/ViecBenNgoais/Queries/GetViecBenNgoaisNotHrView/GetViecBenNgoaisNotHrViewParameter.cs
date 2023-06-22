using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView
{
    public class GetViecBenNgoaisNotHrViewParameter : RequestParameter
    {
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }
}
