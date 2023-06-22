using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCapsC1C2
{
    public class XetDuyetPhuCapsC1C2Parameter
    {
        public string TrangThai { get; set; }
        public IList<XetDuyetPhuCapsC1C2Model> DanhSachXetDuyet { get; set; }
    }
}
