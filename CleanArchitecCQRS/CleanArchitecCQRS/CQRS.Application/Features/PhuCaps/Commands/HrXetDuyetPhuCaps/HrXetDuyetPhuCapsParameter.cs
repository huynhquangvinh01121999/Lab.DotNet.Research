using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.HrXetDuyetPhuCaps
{
    public class HrXetDuyetPhuCapsParameter
    {
        public string HR_TrangThai { get; set; }
        public IList<HrXetDuyetPhuCapModel> DanhSachXetDuyet { get; set; }
    }
}
