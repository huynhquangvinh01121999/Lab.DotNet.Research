using System;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.HrXetDuyetPhuCaps
{
    public class HrXetDuyetPhuCapModel
    {
        public Guid Id { get; set; }
        public DateTime? XD_ThoiGianBatDau { get; set; }
        public DateTime? XD_ThoiGianKetThuc { get; set; }
        public int? XD_SoLanPhuCap { get; set; }
        public int? XD_SoBuoiSang { get; set; }
        public int? XD_SoBuoiChieu { get; set; }
        public int? XD_SoBuoiTrua { get; set; }
        public int? XD_SoQuaDem { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
