using System;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.UpdatePhuCaps
{
    public class UpdatePhuCapsParameter
    {
        public int LoaiPhuCapId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }
    }
}
