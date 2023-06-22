using System;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.HrXetDuyetTangCa
{
    public class HrTangCaXetDuyetModel
    {
        public Guid Id { get; set; }
        public float? SoGioDuocDuyet { get; set; }
        public float? SoGioNgayLe { get; set; }
        public float? SoGioCuoiTuan { get; set; }
        public float? SoGioNgayThuong { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
