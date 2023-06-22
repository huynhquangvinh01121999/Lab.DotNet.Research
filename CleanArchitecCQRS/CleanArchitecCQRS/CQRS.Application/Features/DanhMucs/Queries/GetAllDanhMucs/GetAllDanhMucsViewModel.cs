using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.DanhMucs.Queries.GetAllDanhMucs
{
    public class GetAllDanhMucsViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
    }
}
