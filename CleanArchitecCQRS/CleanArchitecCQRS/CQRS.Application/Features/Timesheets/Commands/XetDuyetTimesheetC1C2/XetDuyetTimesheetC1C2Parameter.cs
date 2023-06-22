using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetC1C2
{
    public class XetDuyetTimesheetC1C2Parameter
    {
        public string TrangThai { get; set; }
        public IList<XetDuyetTimesheetC1C2Model> DanhSachXetDuyet { get; set; }
    }
}
