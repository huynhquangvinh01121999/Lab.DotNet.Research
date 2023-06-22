using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails
{
    public class GetAllGroupMailNhanViensViewModel
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenVN { get; set; }
        public string PhongTenVN { get; set; }
        public string PhongTenJP { get; set; }
        public string BanTenVN { get; set; }
        public string BanTenJP { get; set; }
        public string GhiChu { get; set; }
    }
}
