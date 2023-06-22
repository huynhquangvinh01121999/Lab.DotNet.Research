
using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class DashBoard
    {
        public int TongNhanVien { get; set; }
        public int TongThaiSan { get; set; }
        public int TongThoiViec { get; set; }
        public int TongThuViec { get; set; }
        public int TongDonThoiViec { get; set; }
        public int TongNghiDaiHan { get; set; }
        public int TongTapSu { get; set; }
        public int TongCongTac { get; set; }
        public IList<DashBoard_DanhMuc> TrinhDoHocVans { get; set; }
        public IList<DashBoard_DanhMuc> TrinhDoTiengNhats { get; set; }
        public IList<DashBoard_DanhMuc> QuocTichs { get; set; }
    }

}
