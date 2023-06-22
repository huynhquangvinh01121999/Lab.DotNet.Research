using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCa
{
    public class XetDuyetTangCaParameter
    {
        public string PhanLoai { get; set; }
        public string TrangThai { get; set; }
        public IList<TangCaXetDuyetModel> DanhSachXetDuyet{ get; set; }
    }
}
