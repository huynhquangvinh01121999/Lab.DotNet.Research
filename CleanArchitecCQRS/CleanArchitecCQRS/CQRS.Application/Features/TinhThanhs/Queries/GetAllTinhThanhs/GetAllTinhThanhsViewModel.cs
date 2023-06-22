using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs
{
    public class GetAllTinhThanhsViewModel
    {
        public int Id { get; set; }
        public string TenTinhVN { get; set; }
        public string TenTinhEN { get; set; }
        public string TenTinhJP { get; set; }
        public string MaTinh { get; set; }
    }
}
