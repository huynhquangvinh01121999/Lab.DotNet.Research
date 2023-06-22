using System;

namespace EsuhaiSchedule.Application.Models
{
    public class GetTongHopXetDuyetViewModel
    {
        public Guid? Id { get; set; }
        public string HoTen { get; set; }
        public string EmailCongTy { get; set; }
        public int? SLDonDieuChinh { get; set; }
        public int? SLDonTangCa { get; set; }
        public int? SLDonPhuCap { get; set; }
    }
}
