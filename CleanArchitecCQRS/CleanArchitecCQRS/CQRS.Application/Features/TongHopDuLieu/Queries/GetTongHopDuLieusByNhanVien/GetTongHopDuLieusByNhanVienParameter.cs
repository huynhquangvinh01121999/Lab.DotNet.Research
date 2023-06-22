using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien
{
    public class GetTongHopDuLieusByNhanVienParameter
    {
        public Guid NhanVienId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
