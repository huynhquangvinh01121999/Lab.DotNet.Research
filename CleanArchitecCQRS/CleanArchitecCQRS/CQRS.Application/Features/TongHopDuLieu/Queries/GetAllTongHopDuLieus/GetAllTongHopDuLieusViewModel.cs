using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetAllTongHopDuLieus
{
    public class GetAllTongHopDuLieusViewModel
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public string HoTenNhanVienVN { get; set; }
        public string NhanVienMaNhanVien { get; set; }
        public DateTime NgayLamViec { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string SoThe { get; set; }
        public DateTime? CaLamViec_BatDau { get; set; }
        public DateTime? CaLamViec_KetThuc { get; set; }
        public DateTime? CaLamViec_BatDauNghi { get; set; }
        public DateTime? CaLamViec_KetThucNghi { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public DateTime? DieuChinh_GioVao { get; set; }
        public DateTime? DieuChinh_GioRa { get; set; }
        public string DieuChinh_GhiChu { get; set; }
    }
}
