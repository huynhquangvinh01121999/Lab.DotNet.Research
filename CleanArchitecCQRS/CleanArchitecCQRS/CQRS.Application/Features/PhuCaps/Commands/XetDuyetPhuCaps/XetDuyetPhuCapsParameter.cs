using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCaps
{
    public class XetDuyetPhuCapsParameter
    {
        public string PhanLoai { get; set; }
        public string TrangThai { get; set; }
        public IList<PhuCapXetDuyetModel> DanhSachXetDuyet { get; set; }
    }
}
