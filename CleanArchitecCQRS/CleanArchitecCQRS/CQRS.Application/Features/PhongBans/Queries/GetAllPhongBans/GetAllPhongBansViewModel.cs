using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans
{
    public class GetAllPhongBansViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public Guid? TruongBoPhanId { get; set; }
        public int? Parent { get; set; }
        public string TenGroupMail { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string TruongPhongTenVN { get; set; }
        public string TruongPhongHoTenDemVN { get; set; }
        public int TongNhanVien { get; set; }
        public string PhongBanParentTenVN { get; set; }
        public string PhongBanParentTenJP { get; set; }
    }
}
