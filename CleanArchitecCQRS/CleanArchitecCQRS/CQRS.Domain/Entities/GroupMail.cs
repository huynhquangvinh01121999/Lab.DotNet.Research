using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class GroupMail:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public Guid? NhanVienDeNghiId { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MucDich { get; set; }
        public string MoTa { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public IList<NhanVien_GroupMail> NhanVien_GroupMails { get; set; }
        public NhanVien NhanVienDeNghi { get; set; }
        public PhongBan MailPhongBan { get; set; }
    }
}
