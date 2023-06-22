using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepC1C2
{
    public class XetDuyetNghiPhepC1C2Parameter
    {
        public string TrangThai { get; set; }
        public IList<XetDuyetNghiPhepC1C2Model> DanhSachXetDuyet { get; set; }
    }
}
