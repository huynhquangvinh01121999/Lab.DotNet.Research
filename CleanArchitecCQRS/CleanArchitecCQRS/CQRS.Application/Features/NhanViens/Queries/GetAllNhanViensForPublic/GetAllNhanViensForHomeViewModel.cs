using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic
{
    public class GetAllNhanViensForHomeViewModel
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public string Avatar { get; set; }
        public string TenPhongTenVN { get; set; }
        public string TenPhongTenJP { get; set; }
        public string TenBanTenVN { get; set; }
        public string TenBanTenJP { get; set; }
        public string TenChucVuTenVN { get; set; }
        public string TenChucVuTenJP { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
    }
}
