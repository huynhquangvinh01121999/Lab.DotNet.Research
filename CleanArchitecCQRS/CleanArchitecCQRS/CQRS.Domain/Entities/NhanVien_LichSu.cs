using System;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_LichSu
    {
        public Guid? NhanVienId { get;set; }
        public string IpAddress { get; set; }
        public DateTime? ActionTime { get; set; }
        public string ActionName { get; set; }
        public string ActionTable { get; set; }
        public string ActionFunction { get; set; }
        public bool? IsSuccess { get; set; }
        public string KeyAccess { get; set; }
    }
}
