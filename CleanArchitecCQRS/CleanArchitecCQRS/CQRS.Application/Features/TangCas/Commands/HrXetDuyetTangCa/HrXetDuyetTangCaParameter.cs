using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.HrXetDuyetTangCa
{
    public class HrXetDuyetTangCaParameter
    {
        public string HR_TrangThai { get; set; }
        public IList<HrTangCaXetDuyetModel> DanhSachXetDuyet { get; set; }
    }
}
