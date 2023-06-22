using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class NhanVien_GroupMail:AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid? NhanVienId { get; set; }
        public int? GroupMailId { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public GroupMail GroupMail { get; set; }
    }
}
