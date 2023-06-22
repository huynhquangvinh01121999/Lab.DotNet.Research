using System;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.CreateTangCa
{
    public class CreateTangCaParameter
    {
        public DateTime NgayTangCa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }
        public float? SoGioDangKy { get; set; }
    }
}
