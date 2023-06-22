using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetAllCaLamViecs
{
    public class GetAllCaLamViecsViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public DateTime BatDauNghi { get; set; }
        public DateTime KetThucNghi { get; set; }
        public bool? KhacNgay { get; set; }
        public string GhiChu { get; set; }
    }
}
