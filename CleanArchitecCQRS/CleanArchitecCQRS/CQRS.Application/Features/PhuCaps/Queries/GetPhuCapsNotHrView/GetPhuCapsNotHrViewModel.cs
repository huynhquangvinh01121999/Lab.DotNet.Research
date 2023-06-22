using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView
{
    public class GetPhuCapsNotHrViewModel
    {
        public Guid Id { get; set; }
        //public Guid NhanVienId { get; set; }
        //public int LoaiPhuCapId { get; set; }

        public string HoTenNhanVienVN { get; set; }
        public string LoaiPhuCapTen { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public DateTime Created { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string MoTa { get; set; }

        public short? SoLanPhuCap { get; set; }
        public short? SoBuoiSang { get; set; }
        public short? SoBuoiTrua { get; set; }
        public short? SoBuoiChieu { get; set; }
        public short? SoQuaDem { get; set; }
        public short? SoLanCuoiTuan { get; set; }
        public short? SoLanNgayLe { get; set; }
        public short? SoLanNgayThuong { get; set; }


        public DateTime? XD_ThoiGianBatDau { get; set; }
        public DateTime? XD_ThoiGianKetThuc { get; set; }
        public short? XD_SoLanPhuCap { get; set; }
        public short? XD_SoBuoiSang { get; set; }
        public short? XD_SoBuoiTrua { get; set; }
        public short? XD_SoBuoiChieu { get; set; }
        public short? XD_SoQuaDem { get; set; }


        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public DateTime? NXD1_HanDuyet { get; set; }
        public string NXD1_TrangThai { get; set; }
        public string NXD1_GhiChu { get; set; }
        public bool? IsXetDuyetCap1 { get; set; }
        public bool? NXD1_isHetHanDuyet { get; set; }
        public string NXD1_Ten { get; set; }

        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public DateTime? NXD2_HanDuyet { get; set; }
        public string NXD2_TrangThai { get; set; }
        public string NXD2_GhiChu { get; set; }
        public bool? IsXetDuyetCap2 { get; set; }
        public bool? NXD2_isHetHanDuyet { get; set; }
        public string NXD2_Ten { get; set; }

        public Guid? HRXetDuyetId { get; set; }
        public string HR_TrangThai { get; set; }
        public string HR_GhiChu { get; set; }

        public IList<NghiPhep> NghiPheps { get; set; }
        public IList<ViecBenNgoai> ViecBenNgoais { get; set; }

        public class NghiPhep
        {
            public Guid? Id { get; set; }
            public DateTime? ThoiGianBatDau { get; set; }
            public DateTime? ThoiGianKetThuc { get; set; }
            public float? SoNgayDangKy { get; set; }
            public string TrangThaiNghi { get; set; }
            public string TrangThaiDangKy { get; set; }
            public string MoTa { get; set; }
        }

        public class ViecBenNgoai
        {
            public Guid? Id { get; set; }
            public DateTime? ThoiGianBatDau { get; set; }
            public DateTime? ThoiGianKetThuc { get; set; }
            public float? SoGio { get; set; }
            public string MoTa { get; set; }
            public string LoaiCongTac { get; set; }
        }
    }
}
