using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.ChucVus.Queries.GetAllChucVus
{
    public class GetAllChucVusViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public int? CapBac { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public int TongNhanVien { get; set; }
    }
}
