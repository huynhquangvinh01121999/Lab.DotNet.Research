using EsuhaiHRM.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensParameter : RequestParameter
    {
        public string SortParam { get; set; }
        public string FilterParams { get; set; }
        public string SearchParam { get; set; }
    }
}
