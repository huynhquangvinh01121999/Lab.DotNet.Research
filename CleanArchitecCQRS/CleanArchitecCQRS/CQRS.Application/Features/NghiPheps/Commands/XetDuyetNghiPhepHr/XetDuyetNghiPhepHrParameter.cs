using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepHr
{
    public class XetDuyetNghiPhepHrParameter
    {
        public string HR_TrangThai { get; set; }
        public IList<XetDuyetNghiPhepHrModel> DanhSachXetDuyet { get; set; }
    }
}
