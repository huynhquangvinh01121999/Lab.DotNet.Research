﻿using System;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien
{
    public class GetTangCasByNhanVienViewModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayTangCa { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float? SoGioDangKy { get; set; }
        public float? SoGioNgayLe { get; set; }
        public float? SoGioCuoiTuan { get; set; }
        public float? SoGioNgayThuong { get; set; }
        public float? SoGioDuocDuyet { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }

        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public string NXD1_GhiChu { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_TrangThai { get; set; }
        //public string NXD1_Display { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public string NXD2_GhiChu { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_TrangThai { get; set; }
        //public string NXD2_Display { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }
        //public string HR_Display { get; set; }

        public bool? isDisabled { get; set; }
    }
}