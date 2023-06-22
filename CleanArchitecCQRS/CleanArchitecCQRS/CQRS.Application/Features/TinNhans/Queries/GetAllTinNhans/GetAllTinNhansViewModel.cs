using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.TinNhans.Queries.GetAllTinNhans
{
    public class GetAllTinNhansViewModel
    {
        public Guid Id { get; set; }
        public Guid? NhanVienGuiId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NhanHieu { get; set; }
        public string UrlThongTin { get; set; }
        public DateTime NgayGui { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
    }
}
