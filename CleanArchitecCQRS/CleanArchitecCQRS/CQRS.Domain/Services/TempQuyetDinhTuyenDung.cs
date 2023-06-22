using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Domain.Services
{
    public class TempQuyetDinhTuyenDung
    {
        public TempQuyetDinhTuyenDung() { }
        public DataCollector SQDTD { get; set; } = DataCollector.SetDefault("SQDTD");
        public DataCollector NgayHienTai { get; set; } = DataCollector.SetDefault("NgayHienTai");
        public DataCollector ThangHienTai { get; set; } = DataCollector.SetDefault("ThangHienTai");
        public DataCollector NamHienTai { get; set; } = DataCollector.SetDefault("NamHienTai");
        public DataCollector TienTo { get; set; } = DataCollector.SetDefault("TienTo");
        public DataCollector TenNguoiLaoDongUpper { get; set; } = DataCollector.SetDefault("TenNguoiLaoDongUpper");
        public DataCollector TenNguoiLaoDong { get; set; } = DataCollector.SetDefault("TenNguoiLaoDong");
        public DataCollector TenChucVu { get; set; } = DataCollector.SetDefault("TenChucVu");
        public DataCollector TenPhongBan { get; set; } = DataCollector.SetDefault("TenPhongBan");
        public DataCollector TroCapNhaXa { get; set; } = DataCollector.SetDefault("TroCapNhaXa");
        public DataCollector TroCapDinhDuong { get; set; } = DataCollector.SetDefault("TroCapDinhDuong");
        public DataCollector SoKmNhaXa { get; set; } = DataCollector.SetDefault("SoKmNhaXa");
        public DataCollector NgayChinhLuongLan2 { get; set; } = DataCollector.SetDefault("NgayChinhLuongLan2");
        public DataCollector NgayHieuLuc { get; set; } = DataCollector.SetDefault("NgayHieuLuc");
    }
}