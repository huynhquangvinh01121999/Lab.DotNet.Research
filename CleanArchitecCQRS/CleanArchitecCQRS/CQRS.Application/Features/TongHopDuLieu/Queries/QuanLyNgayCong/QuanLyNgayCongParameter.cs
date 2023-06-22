using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong
{
    public class QuanLyNgayCongParameter : RequestParameter
    {
        public DateTime Thang { get; set; }
        public int PhongId { get; set; }
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
    }
}
