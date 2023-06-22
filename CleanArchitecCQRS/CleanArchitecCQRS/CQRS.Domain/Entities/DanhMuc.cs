using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class DanhMuc:AuditableBaseEntity
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public IList<NhanVien> GioiTinhs { get; set; }
        public IList<NhanVien> HonNhans { get; set; }
        public IList<NhanVien> DanTocs { get; set; }
        public IList<NhanVien> TonGiaos { get; set; }
        public IList<NhanVien> NguyenQuans { get; set; }
        public IList<NhanVien> NoiSinhs { get; set; }
        public IList<NhanVien> QuocTichs { get; set; }
        public IList<NhanVien> TrangThais { get; set; }
        public IList<NhanVien_ThongTinGiaDinh> QuanHeGiaDinhs { get; set; }
        public IList<NhanVien_ThongTinHocVan> TrinhDoHocVans { get; set; }
        public IList<NhanVien_ThongTinHocVan> NhomNganhs { get; set; }
        public IList<NhanVien_HoSo> HoSos { get; set; }
        public IList<NhanVien_HoSo> LoaiHoSos { get; set; }
        public IList<NhanVien_HopDong> LoaiHopDongs { get; set; }
        public IList<NhanVien_DanhGia> LoaiDanhGias { get; set; }
        public IList<ChiNhanh> LoaiChiNhanhs { get; set; }
        public IList<NhanVien_CongTac> TinhTrangCongTacs { get; set; }
        public IList<NhanVien_PhuCap> LoaiPhuCaps { get; set; }
        public IList<NhanVien_ThongTinKhac> LoaiXuatCanhs { get; set; }
        public IList<NhanVien_BangCapChungChi> LoaiBangCapChungChis { get; set; }
    }
}
