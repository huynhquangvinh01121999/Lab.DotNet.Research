using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr
{
    public class GetTimesheetsPhongBanV2HrViewModel
    {
        public Guid? NhanVienId { get; set; }
        public string HoTen { get; set; }
        public IList<Infos> info { get; set; }

        public class Infos
        {
            public int? Stt { get; set; }
            public Guid? Id { get; set; }
            public string NgayLamViec { get; set; }
            public string Thu { get; set; }
            public string NgayCong { get; set; }
            public IList<CaLamViec> Ca1 { get; set; }
            public IList<CaLamViec> Ca2 { get; set; }

            public class CaLamViec
            {
                public DateTime? TGBD_Display { get; set; }
                public DateTime? TGKT_Display { get; set; }
                public bool? isNghiPhep { get; set; }
            }
        }
    }
}
