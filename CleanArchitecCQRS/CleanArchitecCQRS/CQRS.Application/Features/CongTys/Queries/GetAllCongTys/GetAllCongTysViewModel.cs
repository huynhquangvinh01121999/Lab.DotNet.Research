using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys
{
    public class GetAllCongTysViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTyVN { get; set; }
        public string TenCongTyEN { get; set; }
        public string TenCongTyJP { get; set; }
        public string TenVietTat { get; set; }
        public string TenGiamDoc { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public int TongNhanVien { get; set; }
        
    }
}
