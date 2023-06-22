
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Domain.Services
{
    public class TempPhuLucHopDongDieuChinh
    {
        public TempPhuLucHopDongDieuChinh() { }
        public DataCollector SoPLHDDC { get; set; } = DataCollector.SetDefault("SoPLHDDC");
        public DataCollector SoHDLD { get; set; } = DataCollector.SetDefault("SoHDLD");
        public DataCollector NgayKyHDLD { get; set; } = DataCollector.SetDefault("NgayKyHDLD");
        public DataCollector NgayHienTai { get; set; } = DataCollector.SetDefault("NgayHienTai");
        public DataCollector ThangHienTai { get; set; } = DataCollector.SetDefault("ThangHienTai");
        public DataCollector NamHienTai { get; set; } = DataCollector.SetDefault("NamHienTai");
        public DataCollector TenNguoiLaoDongUpper { get; set; } = DataCollector.SetDefault("TenNguoiLaoDongUpper");
        public DataCollector TenNguoiLaoDong { get; set; } = DataCollector.SetDefault("TenNguoiLaoDong");
        public DataCollector GioiTinh { get; set; } = DataCollector.SetDefault("GioiTinh");
        public DataCollector QuocTich { get; set; } = DataCollector.SetDefault("QuocTich");
        public DataCollector NgaySinh { get; set; } = DataCollector.SetDefault("NgaySinh");
        public DataCollector NoiSinh { get; set; } = DataCollector.SetDefault("NoiSinh");
        public DataCollector CMND_So { get; set; } = DataCollector.SetDefault("CMND_So");
        public DataCollector CMND_NoiCap { get; set; } = DataCollector.SetDefault("CMND_NoiCap");
        public DataCollector CMND_NgayCap { get; set; } = DataCollector.SetDefault("CMND_NgayCap");
        public DataCollector DiaChiThuongTru { get; set; } = DataCollector.SetDefault("DiaChiThuongTru");
        public DataCollector GPLD_So { get; set; } = DataCollector.SetDefault("GPLD_So");
        public DataCollector GPLD_NgayCap { get; set; } = DataCollector.SetDefault("GPLD_NgayCap");
        public DataCollector GPLD_NoiCap { get; set; } = DataCollector.SetDefault("GPLD_NoiCap");
        public DataCollector SoPLHD { get; set; } = DataCollector.SetDefault("SoPLHD");
        public DataCollector NgayKyPLHD { get; set; } = DataCollector.SetDefault("NgayKyPLHD");
        public DataCollector ThangKyPLHD { get; set; } = DataCollector.SetDefault("ThangKyPLHD");
        public DataCollector NamKyPLHD { get; set; } = DataCollector.SetDefault("NamKyPLHD");
    }
}