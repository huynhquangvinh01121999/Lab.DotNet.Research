using EsuhaiHRM.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys
{
    public class GetAllCongTysParameter : RequestParameter
    {
        //Add Search Param
        public string SearchValue { get; set; }
    }
}
