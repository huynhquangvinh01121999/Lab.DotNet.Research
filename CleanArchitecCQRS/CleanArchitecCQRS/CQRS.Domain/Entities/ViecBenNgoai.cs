using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Entities
{
    public class ViecBenNgoai : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public int DiemDenId { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string LoaiCongTac { get; set; }
        public string TrangThaiXetDuyet { get; set; }
        public string MoTa { get; set; }
        public int? RowId { get; set; }
        public int? RequestId { get; set; }
        public string DiaDiem { get; set; }
        public Guid? SpId { get; set; }
        public string NguoiGap { get; set; }
        public float? SoGio { get; set; }
        public Guid NhanVienThayTheId { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }

        public NhanVien NhanVien { get; set; }
        public IList<TongHopDuLieu> TongHopDuLieus { get; set; }
        public DiemDen DiemDen { get; set; }
        public NhanVien NhanVienThayThe { get; set; }
    }
}
