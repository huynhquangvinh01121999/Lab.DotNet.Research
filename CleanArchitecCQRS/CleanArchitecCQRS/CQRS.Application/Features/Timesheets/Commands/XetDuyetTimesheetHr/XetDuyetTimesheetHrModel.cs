using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetHr
{
    public class XetDuyetTimesheetHrModel
    {
        public Guid Id { get; set; }
        public DateTime? HR_GioVao { get; set; }
        public DateTime? HR_GioRa { get; set; }
        public string HR_GhiChu { get; set; }
    }
}
