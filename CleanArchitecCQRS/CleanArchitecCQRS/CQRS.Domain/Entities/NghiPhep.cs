using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class NghiPhep : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float? SoNgayDangKy { get; set; }
        public string MoTa { get; set; }
        public string TrangThaiDangKy { get; set; }
        public string TrangThaiNghi { get; set; }
        public int? RowId { get; set; }
        public Guid? SpId { get; set; }
        public string CongViecThayThe { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }

        public Guid? NhanVienThayTheId { get; set; }

        public NhanVien NhanVien { get; set; }
        public IList<TongHopDuLieu> TongHopDuLieus { get; set; }

    }
}
