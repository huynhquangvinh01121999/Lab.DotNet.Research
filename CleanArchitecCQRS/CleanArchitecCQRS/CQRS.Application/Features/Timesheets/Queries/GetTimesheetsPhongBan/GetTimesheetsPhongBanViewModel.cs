using System;
using System.Collections.Generic;
using static EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView.GetTimesheetsHrViewModel;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan
{
    public class GetTimesheetsPhongBanViewModel
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; }
        public IList<TimesheetInfo> info { get; set; }

        public class TimesheetInfo
        {
            public int Stt { get; set; }
            public string NgayLamViec { get; set; }
            public string GioVao { get; set; }
            public string GioRa { get; set; }
            public string Thu { get; set; }
            public string NgayCong { get; set; }
            public IList<NghiPhep> NghiPheps { get; set; }
        }
    }
}
