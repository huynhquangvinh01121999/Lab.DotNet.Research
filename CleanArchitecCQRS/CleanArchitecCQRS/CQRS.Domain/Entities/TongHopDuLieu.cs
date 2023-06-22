using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class TongHopDuLieu : AuditableBaseEntity
    {
        public int Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayLamViec { get; set; }
        public bool? isNgayLe { get; set; }
        public bool? isCuoiTuan { get; set; }

        public DateTime? CaLamViec_BatDau { get; set; }
        public DateTime? CaLamViec_KetThuc { get; set; }
        public DateTime? CaLamViec_BatDauNghi { get; set; }
        public DateTime? CaLamViec_KetThucNghi { get; set; }

        public DateTime? Timesheet_GioVao { get; set; }
        public DateTime? Timesheet_GioRa { get; set; }
        public float? Timesheet_NgayCong { get; set; }
        public float? Timesheet_GioCong { get; set; }

        public float? DiTre { get; set; }
        public float? VeSom { get; set; }

        public float? NgayCong { get; set; }
        public float? Final_GioCong { get; set; }

        public DateTime? Final_GioVao { get; set; }
        public DateTime? Final_GioRa { get; set; }

        public Guid? NghiPhepId { get; set; }
        public DateTime? NghiPhepThoiGianBatDau { get; set; }
        public DateTime? NghiPhepThoiGianKetThuc { get; set; }
        public string NghiPhepTrangThaiNghi { get; set; }
        public float? NghiPhepHopLeNgayCong { get; set; }
        public float? NghiPhepKhongHopLeNgayNghi { get; set; }

        public Guid? ViecBenNgoaiId { get; set; }
        public DateTime? ViecBenNgoaiThoiGianBatDau { get; set; }
        public DateTime? ViecBenNgoaiThoiGianKetThuc { get; set; }
        public float? ViecBenNgoaiNgayCong { get; set; }

        public float? NghiLe_NgayCong { get; set; }

        public Guid? NghiPhep1Id { get; set; }
        public DateTime? NghiPhep1ThoiGianBatDau { get; set; }
        public DateTime? NghiPhep1ThoiGianKetThuc { get; set; }
        public string NghiPhep1TrangThaiNghi { get; set; }
        public Guid? NghiPhep2Id { get; set; }
        public DateTime? NghiPhep2ThoiGianBatDau { get; set; }
        public DateTime? NghiPhep2ThoiGianKetThuc { get; set; }
        public string NghiPhep2TrangThaiNghi { get; set; }

        public NhanVien NhanVien { get; set; }
        public ViecBenNgoai ViecBenNgoai { get; set; }
        public NghiPhep NghiPhep { get; set; }

    }
}