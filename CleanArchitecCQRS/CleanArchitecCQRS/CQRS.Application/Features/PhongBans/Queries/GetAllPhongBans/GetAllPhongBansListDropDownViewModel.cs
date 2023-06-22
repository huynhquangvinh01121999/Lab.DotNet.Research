using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans
{
    public class GetAllPhongBansListDropDownViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public int? Parent { get; set; }
    }
}
