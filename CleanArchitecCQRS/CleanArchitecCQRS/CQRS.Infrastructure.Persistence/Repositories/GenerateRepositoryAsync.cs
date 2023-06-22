using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Domain.Entities;
using System.Collections.Generic;
using System;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Services;
using EsuhaiHRM.Application.Exceptions;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class GenerateRepositoryAsync : IGenerateService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<NhanVien> _nhanViens;
        private readonly DbSet<NhanVien_CMND> _nhanViensCMND;
        private readonly DbSet<NhanVien_GiayPhep> _nhanViensGiayphep;
        private readonly DbSet<NhanVien_HopDong> _nhanViensHopdong;
        private readonly DbSet<DanhMuc> _danhmucs;
        private readonly DbSet<CongTy> _congTys;
        private readonly DbSet<NhanVien_ThoiViec> _nhanvienThoiViecs;

        public GenerateRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _nhanViens = dbContext.Set<NhanVien>();
            _nhanViensCMND = dbContext.Set<NhanVien_CMND>();
            _nhanViensGiayphep = dbContext.Set<NhanVien_GiayPhep>();
            _nhanViensHopdong = dbContext.Set<NhanVien_HopDong>();
            _danhmucs = dbContext.Set<DanhMuc>();
            _congTys = dbContext.Set<CongTy>();
            _nhanvienThoiViecs = dbContext.Set<NhanVien_ThoiViec>();
        }

        private Dictionary<string,string> ListHD = new Dictionary<string, string>() {
             {"Hợp đồng thử việc","HĐTV" }
            ,{"Hợp đồng đào tạo","HĐĐT" }
            ,{"Hợp đồng lao động","HĐLĐ"}
            ,{"Quyết định tuyển dụng","QĐTD"}
            ,{"Phụ lục hợp đồng","PLHĐ"}
            ,{"Phụ lục hợp đồng điều chỉnh","PLHĐĐC"}
            ,{"Quyết định thôi việc","QĐTV"}
            ,{"Biên bản thỏa thuận","BBTT"}
        };

        public static class ListKeyHDTV
        {
            public static readonly string SoHDTV = "SoHDTV";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string QuocTich = "QuocTich";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string NoiSinh = "NoiSinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string GPLD_So = "GPLD_So";
            public static readonly string GPLD_NgayCap = "GPLD_NgayCap";
            public static readonly string GPLD_NoiCap = "GPLD_NoiCap";
            public static readonly string GioiTinh = "GioiTinh";
            public static readonly string NgayBatDauThuViec = "NgayBatDauThuViec";
            public static readonly string NgayKetThucThuViec = "NgayKetThucThuViec";
            public static readonly string LuongThuViec_So = "LuongThuViec_So";
            public static readonly string LuongThuViec_Chu = "LuongThuViec_Chu";
        }

        public static class ListKeyHDDT
        {
            public static readonly string SoHDDT = "SoHDDT";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string QuocTich = "QuocTich";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string NoiSinh = "NoiSinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string GPLD_So = "GPLD_So";
            public static readonly string GPLD_NgayCap = "GPLD_NgayCap";
            public static readonly string GPLD_NoiCap = "GPLD_NoiCap";
            public static readonly string NgayBatDauDaoTao = "NgayBatDauDaoTao";
            public static readonly string NgayKetThucDaoTao = "NgayKetThucDaoTao";
            public static readonly string LuongDaoTao_So = "LuongDaoTao_So";
            public static readonly string LuongDaoTao_Chu = "LuongDaoTao_Chu";
        }

        public static class ListKeyHDLD
        {
            public static readonly string SoHDLD = "SoHDLD";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string QuocTich = "QuocTich";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string NoiSinh = "NoiSinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string GPLD_So = "GPLD_So";
            public static readonly string GPLD_NgayCap = "GPLD_NgayCap";
            public static readonly string GPLD_NoiCap = "GPLD_NoiCap";
            public static readonly string NgayHDCoHieuLuc = "NgayHDCoHieuLuc";
            public static readonly string NgayHDHetHieuLuc = "NgayHDHetHieuLuc";
            public static readonly string Luong = "Luong";
            public static readonly string PhuCapLuong = "PhuCapLuong";
        }

        public static class ListKeyPLHD
        {
            public static readonly string SoPLHD = "SoPLHD";
            public static readonly string SoHDLD = "SoHDLD";
            public static readonly string NgayKyHDLD = "NgayKyHDLD";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string GioiTinh = "GioiTinh";
            public static readonly string QuocTich = "QuocTich";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string NoiSinh = "NoiSinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string GPLD_So = "GPLD_So";
            public static readonly string GPLD_NgayCap = "GPLD_NgayCap";
            public static readonly string GPLD_NoiCap = "GPLD_NoiCap";
            public static readonly string TienHoTro = "TienHoTro";
            public static readonly string HoTroGiuTre = "HoTroGiuTre";
            public static readonly string HoTroNuoiCon = "HoTroNuoiCon";
            public static readonly string HoTroNhaO = "HoTroNhaO";
            public static readonly string HoTroDienThoai = "HoTroDienThoai";
            public static readonly string HoTroComTrua = "HoTroComTrua";
            public static readonly string ThuongKPI = "ThuongKPI";
            public static readonly string ThuongKhac = "ThuongKhac";
        }

        public static class ListKeyPLHDDC
        {
            public static readonly string SoPLHDDC = "SoPLHDDC";
            public static readonly string SoHDLD = "SoHDLD";
            public static readonly string NgayKyHDLD = "NgayKyHDLD";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string GioiTinh = "GioiTinh";
            public static readonly string QuocTich = "QuocTich";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string NoiSinh = "NoiSinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string GPLD_So = "GPLD_So";
            public static readonly string GPLD_NgayCap = "GPLD_NgayCap";
            public static readonly string GPLD_NoiCap = "GPLD_NoiCap";
            public static readonly string SoPLHD = "SoPLHD";
            public static readonly string NgayKyPLHD = "NgayKyPLHD";
            public static readonly string ThangKyPLHD = "ThangKyPLHD";
            public static readonly string NamKyPLHD = "NamKyPLHD";
        }

        public static class ListKeyQDTD
        {
            public static readonly string SQDTD = "SQDTD";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TienTo = "TienTo";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string TenChucVu = "TenChucVu";
            public static readonly string TenPhongBan = "TenPhongBan";
            public static readonly string TroCapNhaXa = "TroCapNhaXa";
            public static readonly string TroCapDinhDuong = "TroCapDinhDuong";
            public static readonly string SoKmNhaXa = "SoKmNhaXa";
            public static readonly string NgayChinhLuongLan2 = "NgayChinhLuongLan2";
            public static readonly string NgayHieuLuc = "NgayHieuLuc";
        }

        public static class ListKeyQDTV
        {
            public static readonly string SoQDTV = "SoQDTV";
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string TienTo = "TienTo";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string TenChucVu = "TenChucVu";
            public static readonly string NgayThoiViec = "NgayThoiViec";
            public static readonly string NgayHieuLuc = "NgayHieuLuc";
        }

        public static class ListKeyBBTT
        {
            public static readonly string NgayHienTai = "NgayHienTai";
            public static readonly string ThangHienTai = "ThangHienTai";
            public static readonly string NamHienTai = "NamHienTai";
            public static readonly string SoQDTV = "SoQDTV";
            public static readonly string TienTo = "TienTo";
            public static readonly string TenNguoiLaoDongUpper = "TenNguoiLaoDongUpper";
            public static readonly string TenNguoiLaoDong = "TenNguoiLaoDong";
            public static readonly string NgaySinh = "NgaySinh";
            public static readonly string CMND_So = "CMND_So";
            public static readonly string CMND_NoiCap = "CMND_NoiCap";
            public static readonly string CMND_NgayCap = "CMND_NgayCap";
            public static readonly string DiaChiThuongTru = "DiaChiThuongTru";
            public static readonly string NgayThoiViec = "NgayThoiViec";
        }

        public async Task<string> GenerateMaNhanVien(int congTyId)
        {
            string maNV = "";
            var congTy = _dbContext.CongTys.SingleOrDefault(n => n.Id == congTyId);
            if (congTy != null)
            {
                string _code = congTy.Code;
                var listNVs = _dbContext.NhanViens.Where(nv => nv.MaNhanVien.StartsWith(_code)).OrderByDescending(nv => nv.MaNhanVien);

                if (listNVs.Count() > 0)
                {
                    string _stt = listNVs.FirstOrDefault().MaNhanVien.Substring(_code.Length, 3);

                    int _intSTT = int.Parse(_stt) + 1;

                    maNV = string.Format("{0}{1}", _code, _intSTT.ToString().PadLeft(3, '0'));
                }
                else
                {
                    maNV = _code + "001";
                }
            }

            return await Task.FromResult(maNV);
        }

        public async Task<Response<string>> GenerateResult(int congTyId ,DateTime? ngayKy,string tenLoai)
        {
            var isThoiViec = false;
            //Get Code CongTy
            var codeCongTy = _congTys.Where(ct => ct.Id == congTyId)
                                     .Select(ct => ct.Code)
                                     .FirstOrDefault();
            if(codeCongTy == null)
            {
                return await Task.FromResult(new Response<string>("CongTy Not Found!"));
            }

            //Get LoaiHopDongId
            var LoaiHDInfo = _danhmucs.Where(dm => dm.PhanLoai == "LoaiHopDong"
                                                && dm.TenVN == tenLoai)
                                      .Select(dm => new { dm.Id, dm.TenVN })
                                      .FirstOrDefault();
            if (LoaiHDInfo == null)
            {
                if (!ListHD.ContainsKey(tenLoai))
                {
                    return await Task.FromResult(new Response<string>("TenLoaiVanBan Not Found!"));
                }
                else
                {
                    isThoiViec = true;
                }
            }

            //GetYear ky Hop Dong
            var namKyHopDong = ngayKy?.Year;

            //GetLast Char
            var lastChar = ListHD[tenLoai];

            //GetCount nhanVien
            var countNhanVien = 0;
            if (isThoiViec)
            {
                //GetCount NhanVien Ky Hop Dong Thoi Viec Trong Nam hien tai
                countNhanVien = _nhanvienThoiViecs.Where(hd => hd.NgayKy.Value.Year == namKyHopDong
                                                                        && hd.SoQuyetDinh != null)
                                                  .Count() + 1;
            }
            else
            {
                //GetCount NhanVien Ky Hop Dong Trong Nam hien tai
                countNhanVien = _nhanViensHopdong.Where(hd => hd.NgayKy.Value.Year == namKyHopDong
                                                                    && hd.LoaiHopDongId == LoaiHDInfo.Id
                                                                    && hd.SoHopDong != null)
                                                 .Count() + 1;
            }
            return await Task.FromResult(new Response<string>(string.Format("{0}.{1}/{2}.{3}"
                                                                              , countNhanVien.ToString().PadLeft(3, Convert.ToChar("0"))
                                                                              , namKyHopDong.ToString().Substring(2)
                                                                              , codeCongTy
                                                                              , lastChar), message: null));
        }

        public async Task<Response<TempHopDongThuViec>> GenerateHopDongThuViec(Guid nhanvienId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVien GiayPhep
            var nhanvienGiayphep = await ThongTinNhanVienGiayPhep(nhanvienId);

            //Get NhanVien HopDong
            var nhanvienHopDong = await ThongTinNhanVienHopDong(nhanvienId, null, "Hợp đồng thử việc");

            var resultData = new TempHopDongThuViec();
            if (nhanvien != null)
            {
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyHDTV.TenNguoiLaoDongUpper)
                                                                  , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyHDTV.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.QuocTich = new DataCollector(GetKeyData(ListKeyHDTV.QuocTich), nhanvien.QuocTich.TenVN);
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyHDTV.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.NoiSinh = new DataCollector(GetKeyData(ListKeyHDTV.NoiSinh), nhanvien.NoiSinh.TenVN);
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyHDTV.DiaChiThuongTru), nhanvien.DiaChiHienTai);
                resultData.GioiTinh = new DataCollector(GetKeyData(ListKeyHDTV.GioiTinh), nhanvien.GioiTinh.TenVN);
            }
            if (nhanvienHopDong != null)
            {
                resultData.SoHDTV = new DataCollector(GetKeyData(ListKeyHDTV.SoHDTV), nhanvienHopDong.SoHopDong);
                resultData.NgayBatDauThuViec = new DataCollector(GetKeyData(ListKeyHDTV.NgayBatDauThuViec), GetDateSlash(nhanvienHopDong.TuNgay));
                resultData.NgayKetThucThuViec = new DataCollector(GetKeyData(ListKeyHDTV.NgayKetThucThuViec), GetDateSlash(nhanvienHopDong.DenNgay));
            }
            if (nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyHDTV.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyHDTV.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyHDTV.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(nhanvienGiayphep != null)
            {
                resultData.GPLD_So = new DataCollector(GetKeyData(ListKeyHDTV.GPLD_So), nhanvienGiayphep.SoGiayPhep);
                resultData.GPLD_NgayCap = new DataCollector(GetKeyData(ListKeyHDTV.GPLD_NgayCap), GetDateSlash(nhanvienGiayphep.TuNgay));
                //resultData.GPLD_NoiCap = new DataCollector(GetKeyData(ListKeyHDTV.GPLD_NoiCap), "");
                //resultData.LuongThuViec_So = new DataCollector(GetKeyData(ListKeyHDTV.LuongThuViec_So), "");
                //resultData.LuongThuViec_Chu = new DataCollector(GetKeyData(ListKeyHDTV.LuongThuViec_Chu), "");
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyHDTV.NgayHienTai), GetFormatNumber(DateTime.Now.Day,2,'0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyHDTV.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyHDTV.NamHienTai), DateTime.Now.Year);

            return await Task.FromResult(new Response<TempHopDongThuViec>(resultData));
        }

        public async Task<Response<TempHopDongDaoTao>> GenerateHopDongDaoTao(Guid nhanvienId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVien GiayPhep
            var nhanvienGiayphep = await ThongTinNhanVienGiayPhep(nhanvienId);

            //Get NhanVien HopDong
            var nhanvienHopDong = await ThongTinNhanVienHopDong(nhanvienId, null, "Hợp đồng đào tạo");

            var resultData = new TempHopDongDaoTao();
            if(nhanvien != null)
            {
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyHDDT.TenNguoiLaoDongUpper)
                                                              , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyHDDT.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.QuocTich = new DataCollector(GetKeyData(ListKeyHDDT.QuocTich), nhanvien.QuocTich.TenVN);
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyHDDT.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.NoiSinh = new DataCollector(GetKeyData(ListKeyHDDT.NoiSinh), nhanvien.NoiSinh.TenVN);
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyHDDT.DiaChiThuongTru), nhanvien.DiaChiHienTai);
            }
            if(nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyHDDT.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyHDDT.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyHDDT.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(nhanvienGiayphep != null)
            {
                resultData.GPLD_So = new DataCollector(GetKeyData(ListKeyHDDT.GPLD_So), nhanvienGiayphep.SoGiayPhep);
                resultData.GPLD_NgayCap = new DataCollector(GetKeyData(ListKeyHDDT.GPLD_NgayCap), GetDateSlash(nhanvienGiayphep.TuNgay));
                //resultData.GPLD_NoiCap = new DataCollector(GetKeyData(ListKeyHDDT.GPLD_NoiCap), "");
            }
            if(nhanvienHopDong != null)
            {
                resultData.SoHDDT = new DataCollector(GetKeyData(ListKeyHDDT.SoHDDT), nhanvienHopDong.SoHopDong);
                resultData.NgayBatDauDaoTao = new DataCollector(GetKeyData(ListKeyHDDT.NgayBatDauDaoTao), GetDateSlash(nhanvienHopDong.TuNgay));
                resultData.NgayKetThucDaoTao = new DataCollector(GetKeyData(ListKeyHDDT.NgayKetThucDaoTao), GetDateSlash(nhanvienHopDong.DenNgay));
                //resultData.LuongDaoTao_So = new DataCollector(GetKeyData(ListKeyHDDT.LuongDaoTao_So), "");
                //resultData.LuongDaoTao_Chu = new DataCollector(GetKeyData(ListKeyHDDT.LuongDaoTao_Chu), "");
            }
            
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyHDDT.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyHDDT.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyHDDT.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempHopDongDaoTao>(resultData));
        }

        public async Task<Response<TempHopDongLaoDong>> GenerateHopDongLaoDong(Guid nhanvienId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVien GiayPhep
            var nhanvienGiayphep = await ThongTinNhanVienGiayPhep(nhanvienId);

            //Get NhanVien HopDong
            var nhanvienHopDong = await ThongTinNhanVienHopDong(nhanvienId, null, "Hợp đồng lao động");

            var resultData = new TempHopDongLaoDong();
            if(nhanvien != null)
            {
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyHDLD.TenNguoiLaoDongUpper)
                                                              , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyHDLD.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.QuocTich = new DataCollector(GetKeyData(ListKeyHDLD.QuocTich), nhanvien.QuocTich.TenVN);
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyHDLD.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.NoiSinh = new DataCollector(GetKeyData(ListKeyHDLD.NoiSinh), nhanvien.NoiSinh.TenVN);
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyHDLD.DiaChiThuongTru), nhanvien.DiaChiHienTai);
            }
            if(nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyHDLD.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyHDLD.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyHDLD.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(nhanvienGiayphep != null)
            {
                resultData.GPLD_So = new DataCollector(GetKeyData(ListKeyHDLD.GPLD_So),nhanvienGiayphep.SoGiayPhep);
                resultData.GPLD_NgayCap = new DataCollector(GetKeyData(ListKeyHDLD.GPLD_NgayCap), GetDateSlash(nhanvienGiayphep.TuNgay));
                //resultData.GPLD_NoiCap = new DataCollector(GetKeyData(ListKeyHDLD.GPLD_NoiCap), "");
            }
            if(nhanvienHopDong != null)
            {
                resultData.SoHDLD = new DataCollector(GetKeyData(ListKeyHDLD.SoHDLD), nhanvienHopDong.SoHopDong);
                resultData.NgayHDCoHieuLuc = new DataCollector(GetKeyData(ListKeyHDLD.NgayHDCoHieuLuc), GetDateSlash(nhanvienHopDong.TuNgay));
                resultData.NgayHDHetHieuLuc = new DataCollector(GetKeyData(ListKeyHDLD.NgayHDHetHieuLuc), GetDateSlash(nhanvienHopDong.DenNgay));
                //resultData.Luong = new DataCollector(GetKeyData(ListKeyHDLD.Luong), "");
                //resultData.PhuCapLuong = new DataCollector(GetKeyData(ListKeyHDLD.PhuCapLuong), "");
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyHDLD.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyHDLD.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyHDLD.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempHopDongLaoDong>(resultData));
        }

        public async Task<Response<TempPhuLucHopDong>> GeneratePhuLucHopDong(Guid nhanvienId, int hopdongId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVien GiayPhep
            var nhanvienGiayphep = await ThongTinNhanVienGiayPhep(nhanvienId);

            //Get NhanVien HopDong
            var hopdongLaoDong = await ThongTinNhanVienHopDong(nhanvienId, hopdongId, "Hợp đồng lao động");

            //Get NhanVien PhuLucHopDong
            var nhanvienHopDong = await ThongTinNhanVienHopDong(nhanvienId, null, "Phụ lục hợp đồng");

            var resultData = new TempPhuLucHopDong();
            if(nhanvien != null)
            {
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyPLHD.TenNguoiLaoDongUpper)
                                                              , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyPLHD.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.GioiTinh = new DataCollector(GetKeyData(ListKeyPLHD.GioiTinh), nhanvien.GioiTinh.TenVN);
                resultData.QuocTich = new DataCollector(GetKeyData(ListKeyPLHD.QuocTich), nhanvien.QuocTich.TenVN);
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyPLHD.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.NoiSinh = new DataCollector(GetKeyData(ListKeyPLHD.NoiSinh), nhanvien.NoiSinh.TenVN);
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyPLHD.DiaChiThuongTru), nhanvien.DiaChiHienTai);
            }
            if(nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyPLHD.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyPLHD.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyPLHD.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(nhanvienGiayphep != null)
            {
                resultData.GPLD_So = new DataCollector(GetKeyData(ListKeyPLHD.GPLD_So), nhanvienGiayphep.SoGiayPhep);
                resultData.GPLD_NgayCap = new DataCollector(GetKeyData(ListKeyPLHD.GPLD_NgayCap), GetDateSlash(nhanvienGiayphep.TuNgay));
                //resultData.GPLD_NoiCap = new DataCollector(GetKeyData(ListKeyPLHD.GPLD_NoiCap), "");
            }
            if(hopdongLaoDong != null)
            {
                resultData.SoHDLD = new DataCollector(GetKeyData(ListKeyPLHD.SoHDLD), hopdongLaoDong.SoHopDong);
                resultData.NgayKyHDLD = new DataCollector(GetKeyData(ListKeyPLHD.NgayKyHDLD), GetDateSlash(hopdongLaoDong.NgayKy));
            }
            if(nhanvienHopDong != null)
            {
                resultData.SoPLHD = new DataCollector(GetKeyData(ListKeyPLHD.SoPLHD), nhanvienHopDong.SoHopDong);
                //resultData.TienHoTro = new DataCollector(GetKeyData(ListKeyPLHD.TienHoTro), "");
                //resultData.HoTroGiuTre = new DataCollector(GetKeyData(ListKeyPLHD.HoTroGiuTre), "");
                //resultData.HoTroNuoiCon = new DataCollector(GetKeyData(ListKeyPLHD.HoTroNuoiCon), "");
                //resultData.HoTroNhaO = new DataCollector(GetKeyData(ListKeyPLHD.HoTroNhaO), "");
                //resultData.HoTroDienThoai = new DataCollector(GetKeyData(ListKeyPLHD.HoTroDienThoai), "");
                //resultData.HoTroComTrua = new DataCollector(GetKeyData(ListKeyPLHD.HoTroComTrua), "");
                //resultData.ThuongKPI = new DataCollector(GetKeyData(ListKeyPLHD.ThuongKPI), "");
                //resultData.ThuongKhac = new DataCollector(GetKeyData(ListKeyPLHD.ThuongKhac), "");
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyPLHD.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyPLHD.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyPLHD.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempPhuLucHopDong>(resultData));
        }

        public async Task<Response<TempQuyetDinhTuyenDung>> GenerateQuyetDinhTuyenDung(Guid nhanvienId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien PhuLucHopDong
            var nhanvienHopDong = await ThongTinNhanVienHopDong(nhanvienId, null, "Quyết định tuyển dụng");

            var resultData = new TempQuyetDinhTuyenDung();
            if(nhanvien != null)
            {
                resultData.TienTo = new DataCollector(GetKeyData(ListKeyQDTD.TienTo), nhanvien.GioiTinhId == 1 ? "Ông" : "Bà");
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyQDTD.TenNguoiLaoDongUpper)
                                                                  , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyQDTD.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.TenChucVu = new DataCollector(GetKeyData(ListKeyQDTD.TenChucVu), nhanvien.TenChucVu.TenVN);
                resultData.TenPhongBan = new DataCollector(GetKeyData(ListKeyQDTD.TenPhongBan), nhanvien.TenPhong.TenVN);
            }
            if(nhanvienHopDong != null)
            {
                resultData.SQDTD = new DataCollector(GetKeyData(ListKeyQDTD.SQDTD), nhanvienHopDong.SoHopDong);
                resultData.NgayChinhLuongLan2 = new DataCollector(GetKeyData(ListKeyQDTD.NgayChinhLuongLan2), GetDateSlash(nhanvienHopDong.NgayKy.Value.AddMonths(4)));
                resultData.NgayHieuLuc = new DataCollector(GetKeyData(ListKeyQDTD.NgayHieuLuc), GetDateSlash(nhanvienHopDong.NgayKy));
                //resultData.TroCapNhaXa = new DataCollector(GetKeyData(ListKeyQDTD.TroCapNhaXa), "");
                //resultData.TroCapDinhDuong = new DataCollector(GetKeyData(ListKeyQDTD.TroCapDinhDuong), "");
                //resultData.SoKmNhaXa = new DataCollector(GetKeyData(ListKeyQDTD.SoKmNhaXa), "");
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyQDTD.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyQDTD.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyQDTD.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempQuyetDinhTuyenDung>(resultData));
        }

        public async Task<Response<TempPhuLucHopDongDieuChinh>> GeneratePhuLucHopDongDieuChinh(Guid nhanvienId, int hopdongId, int phulucId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVien GiayPhep
            var nhanvienGiayphep = await ThongTinNhanVienGiayPhep(nhanvienId);

            //Get NhanVien HopDong
            var hopdongLaoDong = await ThongTinNhanVienHopDong(nhanvienId, hopdongId, "Hợp đồng lao động");

            var phulucHopDong = await ThongTinNhanVienHopDong(nhanvienId, phulucId, "Phụ lục hợp đồng");

            var phulucHopDongDc = await ThongTinNhanVienHopDong(nhanvienId, null, "Phụ lục hợp đồng điều chỉnh");

            var resultData = new TempPhuLucHopDongDieuChinh();
            if (nhanvien != null)
            {
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyPLHDDC.TenNguoiLaoDongUpper)
                                                              , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyPLHDDC.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.GioiTinh = new DataCollector(GetKeyData(ListKeyPLHDDC.GioiTinh), nhanvien.GioiTinh.TenVN);
                resultData.QuocTich = new DataCollector(GetKeyData(ListKeyPLHDDC.QuocTich), nhanvien.QuocTich.TenVN);
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyPLHDDC.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.NoiSinh = new DataCollector(GetKeyData(ListKeyPLHDDC.NoiSinh), nhanvien.NoiSinh.TenVN);
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyPLHDDC.DiaChiThuongTru), nhanvien.DiaChiHienTai);
            }
            if(nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyPLHDDC.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyPLHDDC.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyPLHDDC.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(nhanvienGiayphep != null)
            {
                resultData.GPLD_So = new DataCollector(GetKeyData(ListKeyPLHDDC.GPLD_So), nhanvienGiayphep.SoGiayPhep);
                resultData.GPLD_NgayCap = new DataCollector(GetKeyData(ListKeyPLHDDC.GPLD_NgayCap), GetDateSlash(nhanvienGiayphep.TuNgay));
                //resultData.GPLD_NoiCap = new DataCollector(GetKeyData(ListKeyPLHDDC.GPLD_NoiCap), "");
            }
            if(hopdongLaoDong != null)
            {
                resultData.SoHDLD = new DataCollector(GetKeyData(ListKeyPLHDDC.SoHDLD), hopdongLaoDong.SoHopDong);
                resultData.NgayKyHDLD = new DataCollector(GetKeyData(ListKeyPLHDDC.NgayKyHDLD), GetDateSlash(hopdongLaoDong.NgayKy));
            }
            if(phulucHopDong != null)
            {
                resultData.SoPLHD = new DataCollector(GetKeyData(ListKeyPLHDDC.SoPLHD), phulucHopDong.SoHopDong);
                resultData.NgayKyPLHD = new DataCollector(GetKeyData(ListKeyPLHDDC.NgayKyPLHD), phulucHopDong.NgayKy.Value.Day);
                resultData.ThangKyPLHD = new DataCollector(GetKeyData(ListKeyPLHDDC.ThangKyPLHD), phulucHopDong.NgayKy.Value.Month);
                resultData.NamKyPLHD = new DataCollector(GetKeyData(ListKeyPLHDDC.NamKyPLHD), phulucHopDong.NgayKy.Value.Year);
            }
            if (phulucHopDongDc != null)
            {
                resultData.SoPLHDDC = new DataCollector(GetKeyData(ListKeyPLHDDC.SoPLHDDC), phulucHopDongDc.SoHopDong);
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyPLHDDC.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyPLHDDC.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyPLHDDC.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempPhuLucHopDongDieuChinh>(resultData));
        }

        public async Task<Response<TempQuyetDinhThoiViec>> GenerateQuyetDinhThoiViec(Guid nhanvienId, int thoiviecId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien ThoiViec
            var nhanvienThoiViec = await ThongTinNhanVienThoiViec(nhanvienId, thoiviecId);

            var resultData = new TempQuyetDinhThoiViec();
            if(nhanvien != null)
            {
                resultData.TienTo = new DataCollector(GetKeyData(ListKeyQDTV.TienTo), nhanvien.GioiTinhId == 1 ? "Ông" : "Bà");
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyQDTV.TenNguoiLaoDongUpper)
                                                                  , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyQDTV.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.TenChucVu = new DataCollector(GetKeyData(ListKeyQDTV.TenChucVu), nhanvien.TenChucVu.TenVN);
            }
            if(nhanvienThoiViec != null)
            {
                resultData.SoQDTV = new DataCollector(GetKeyData(ListKeyQDTV.SoQDTV), nhanvienThoiViec.SoQuyetDinh);
                resultData.NgayThoiViec = new DataCollector(GetKeyData(ListKeyQDTV.NgayThoiViec), GetDateSlash(nhanvienThoiViec.NgayThoiViec));
                resultData.NgayHieuLuc = new DataCollector(GetKeyData(ListKeyQDTV.NgayHieuLuc), GetDateSlash(nhanvienThoiViec.NgayThoiViec));
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyQDTV.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyQDTV.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyQDTV.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempQuyetDinhThoiViec>(resultData));
        }

        public async Task<Response<TempBienBanThoaThuan>> GenerateBienBanThoaThuan(Guid nhanvienId, int thoiviecId)
        {
            //Get NhanVien info
            var nhanvien = await ThongTinNhanVien(nhanvienId);

            //Get NhanVien_CMND
            var nhanvienCmnd = await ThongTinNhanVienCMND(nhanvienId);

            //Get NhanVienThoiViec
            var quyetDinhThoiViec = await ThongTinNhanVienThoiViec(nhanvienId, thoiviecId);

            var resultData = new TempBienBanThoaThuan();
            if(nhanvien != null)
            {
                resultData.TienTo = new DataCollector(GetKeyData(ListKeyBBTT.TienTo), nhanvien.GioiTinhId == 1 ? "Ông" : "Bà");
                resultData.TenNguoiLaoDongUpper = new DataCollector(GetKeyData(ListKeyBBTT.TenNguoiLaoDongUpper)
                                                                  , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN).ToUpper());
                resultData.TenNguoiLaoDong = new DataCollector(GetKeyData(ListKeyBBTT.TenNguoiLaoDong)
                                                             , string.Format("{0} {1}", nhanvien.HoTenDemVN, nhanvien.TenVN));
                resultData.NgaySinh = new DataCollector(GetKeyData(ListKeyBBTT.NgaySinh), GetDateSlash(nhanvien.NgaySinh));
                resultData.DiaChiThuongTru = new DataCollector(GetKeyData(ListKeyBBTT.DiaChiThuongTru), nhanvien.HoKhau);
            }
            if(nhanvienCmnd != null)
            {
                resultData.CMND_So = new DataCollector(GetKeyData(ListKeyBBTT.CMND_So), nhanvienCmnd.SoCmnd);
                resultData.CMND_NoiCap = new DataCollector(GetKeyData(ListKeyBBTT.CMND_NoiCap), nhanvienCmnd.CMNDNoiCap.TenTinhVN);
                resultData.CMND_NgayCap = new DataCollector(GetKeyData(ListKeyBBTT.CMND_NgayCap), GetDateSlash(nhanvienCmnd.NgayCap));
            }
            if(quyetDinhThoiViec != null)
            {
                resultData.SoQDTV = new DataCollector(GetKeyData(ListKeyBBTT.SoQDTV), quyetDinhThoiViec.SoQuyetDinh);
                resultData.NgayThoiViec = new DataCollector(GetKeyData(ListKeyBBTT.NgayThoiViec), GetDateSlash(quyetDinhThoiViec.NgayThoiViec));
            }
            resultData.NgayHienTai = new DataCollector(GetKeyData(ListKeyBBTT.NgayHienTai), GetFormatNumber(DateTime.Now.Day, 2, '0'));
            resultData.ThangHienTai = new DataCollector(GetKeyData(ListKeyBBTT.ThangHienTai), GetFormatNumber(DateTime.Now.Month, 2, '0'));
            resultData.NamHienTai = new DataCollector(GetKeyData(ListKeyBBTT.NamHienTai), DateTime.Now.Year);
            
            return await Task.FromResult(new Response<TempBienBanThoaThuan>(resultData));
        }
        
        public string GetKeyData(string name)
        {
            return string.Format("«{0}»", name);
        }
        
        public string GetDateSlash(DateTime? date)
        {
            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
        }

        public string GetFormatNumber(int number,int totalSize, char character)
        {
            return Convert.ToString(number).PadLeft(totalSize, character);
        }

        public async Task<NhanVien> ThongTinNhanVien (Guid nhanvienId)
        {
            var reusltData = _nhanViens.Where(nv => nv.Id == nhanvienId
                                                 && nv.Deleted != true)
                                       .Include(nv => nv.GioiTinh)
                                       .Include(nv => nv.QuocTich)
                                       .Include(nv => nv.NoiSinh)
                                       .Include(nv => nv.TenChucVu)
                                       .Include(nv => nv.TenPhong)
                                       .FirstOrDefault();
            return await Task.FromResult(reusltData);
        }

        public async Task<NhanVien_CMND> ThongTinNhanVienCMND(Guid nhanvienId)
        {
            var reusltData = _nhanViensCMND.Where(nv => nv.NhanVienId == nhanvienId
                                                     && nv.Deleted != true
                                                     && nv.HieuLuc == true)
                                           .Include(nv => nv.CMNDNoiCap)
                                           .FirstOrDefault();
            return await Task.FromResult(reusltData);
        }

        public async Task<NhanVien_GiayPhep> ThongTinNhanVienGiayPhep(Guid nhanvienId)
        {
            var reusltData = _nhanViensGiayphep.Where(nv => nv.NhanVienId == nhanvienId
                                                         && nv.Deleted != true
                                                         && nv.DenNgay >= DateTime.Now)
                                               .FirstOrDefault();
            return await Task.FromResult(reusltData);
        }

        public async Task<NhanVien_HopDong> ThongTinNhanVienHopDong(Guid nhanvienId, int? hopdongId, string tenLoaiHd)
        {
            var loaiHdId = 0;
            if (tenLoaiHd != null)
            {
                loaiHdId = _danhmucs.Where(dm => dm.PhanLoai == "LoaiHopDong"
                                              && dm.TenVN == tenLoaiHd)
                                    .Select(dm => dm.Id)
                                    .FirstOrDefault();
            }
            var getData = _nhanViensHopdong.Where(nv => nv.NhanVienId == nhanvienId
                                                        && nv.Deleted != true);
            if(loaiHdId != 0)
            {
                getData = getData.Where(nv => nv.LoaiHopDongId == loaiHdId);
            }
            if(hopdongId != null)
            {
                getData = getData.Where(nv => nv.Id == hopdongId);
            }
            var resultData = getData.FirstOrDefault();
            return await Task.FromResult(resultData);
        }

        public async Task<NhanVien_ThoiViec> ThongTinNhanVienThoiViec(Guid nhanvienId, int hopdongId)
        {
            var reusltData = _nhanvienThoiViecs.Where(nv => nv.NhanVienId == nhanvienId
                                                         && nv.Deleted != true
                                                         && nv.Id == hopdongId)
                                               .FirstOrDefault();
            return await Task.FromResult(reusltData);
        }

    }
}
