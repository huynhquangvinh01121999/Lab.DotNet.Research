using System;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.CreateViecBenNgoai
{
    public class CreateViecBenNgoaiParameters
    {
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string LoaiCongTac { get; set; }
        public string MoTa { get; set; }
        public string DiaDiem { get; set; }
        public string NguoiGap { get; set; }
        public float SoGio { get; set; }
        public int DiemDenId { get; set; }
        public Guid NhanVienThayTheId { get; set; }
    }
}
