using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Domain.Services
{
    public class TempQuyetDinhThoiViec
    {
        public TempQuyetDinhThoiViec() { }
        public DataCollector SoQDTV { get; set; } = DataCollector.SetDefault("SoQDTV");
        public DataCollector NgayHienTai { get; set; } = DataCollector.SetDefault("NgayHienTai");
        public DataCollector ThangHienTai { get; set; } = DataCollector.SetDefault("ThangHienTai");
        public DataCollector NamHienTai { get; set; } = DataCollector.SetDefault("NamHienTai");
        public DataCollector TienTo { get; set; } = DataCollector.SetDefault("TienTo");
        public DataCollector TenNguoiLaoDongUpper { get; set; } = DataCollector.SetDefault("TenNguoiLaoDongUpper");
        public DataCollector TenNguoiLaoDong { get; set; } = DataCollector.SetDefault("TenNguoiLaoDong");
        public DataCollector TenChucVu { get; set; } = DataCollector.SetDefault("TenChucVu");
        public DataCollector NgayThoiViec { get; set; } = DataCollector.SetDefault("NgayThoiViec");
        public DataCollector NgayHieuLuc { get; set; } = DataCollector.SetDefault("NgayHieuLuc");
    }
}