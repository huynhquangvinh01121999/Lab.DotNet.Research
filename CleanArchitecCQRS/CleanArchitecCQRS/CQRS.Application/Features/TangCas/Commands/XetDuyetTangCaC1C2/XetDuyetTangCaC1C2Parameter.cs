using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCaC1C2
{
    public class XetDuyetTangCaC1C2Parameter
    {
        public string TrangThai { get; set; }
        public IList<XetDuyetTangCaC1C2Model> DanhSachXetDuyet { get; set; }
    }
}
