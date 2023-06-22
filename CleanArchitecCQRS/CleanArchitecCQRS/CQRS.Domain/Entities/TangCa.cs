using EsuhaiHRM.Domain.Common;
using System;

namespace EsuhaiHRM.Domain.Entities
{
    public class TangCa : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayTangCa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float? SoGioDangKy { get; set; }
        public string MoTa { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
        public float? SoGioDuocDuyet { get; set; }

        public float? SoGioNgayLe { get; set; }
        public float? SoGioCuoiTuan { get; set; }
        public float? SoGioNgayThuong { get; set; }
        public string TrangThai { get; set; }

        public int? EsuhaiHrId { get; set; }

        public NhanVien NhanVien { get; set; }
    }
}