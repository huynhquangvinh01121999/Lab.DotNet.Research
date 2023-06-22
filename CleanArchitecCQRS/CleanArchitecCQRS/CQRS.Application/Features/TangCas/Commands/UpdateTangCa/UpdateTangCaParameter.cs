using System;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.UpdateTangCa
{
    public class UpdateTangCaParameter
    {
        public DateTime NgayTangCa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float? SoGioDangKy { get; set; }
        public string MoTa { get; set; }
    }
}
