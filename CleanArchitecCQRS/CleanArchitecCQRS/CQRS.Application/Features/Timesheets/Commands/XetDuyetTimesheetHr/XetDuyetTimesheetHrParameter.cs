using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetHr
{
    public class XetDuyetTimesheetHrParameter
    {
        public string HR_TrangThai { get; set; }
        public IList<XetDuyetTimesheetHrModel> DanhSachXetDuyet { get; set; }
    }
}
