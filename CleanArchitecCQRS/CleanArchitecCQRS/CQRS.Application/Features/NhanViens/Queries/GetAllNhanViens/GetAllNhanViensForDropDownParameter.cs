using EsuhaiHRM.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensForDropDownParameter : RequestParameter
    {
        public int? PhongBanId { get; set; }
    }
}
