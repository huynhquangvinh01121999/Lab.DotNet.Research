using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Services;
using System;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface IGenerateService
    {
        //MaNhanVien
        Task<string> GenerateMaNhanVien(int CongtyId);

        //HopDongThuViec
        //HopDongDaoTao
        //HopDongDaoTao
        //QuyetDinhTuyenDung
        //PhuLucHopDong
        //QuyetDinhThoiViec
        //BienBanThoaThuan
        Task<Response<string>> GenerateResult(int CongTyId, DateTime? NgayKy, string TenLoai);
        Task<Response<TempHopDongThuViec>> GenerateHopDongThuViec(Guid NhanVienId);
        Task<Response<TempHopDongDaoTao>> GenerateHopDongDaoTao(Guid NhanVienId);
        Task<Response<TempHopDongLaoDong>> GenerateHopDongLaoDong(Guid NhanVienId);
        Task<Response<TempPhuLucHopDong>> GeneratePhuLucHopDong(Guid NhanVienId, int hopDongId);
        Task<Response<TempQuyetDinhTuyenDung>> GenerateQuyetDinhTuyenDung(Guid NhanVienId);
        Task<Response<TempPhuLucHopDongDieuChinh>> GeneratePhuLucHopDongDieuChinh(Guid NhanVienId, int hopDongId, int phulucId);
        Task<Response<TempQuyetDinhThoiViec>> GenerateQuyetDinhThoiViec(Guid NhanVienId, int ThoiViecId);
        Task<Response<TempBienBanThoaThuan>> GenerateBienBanThoaThuan(Guid NhanVienId,int ThoiViecId);
        string GetKeyData(string name);
        string GetDateSlash(DateTime? date);
        string GetFormatNumber(int number, int totalSize, char character);
        Task<NhanVien> ThongTinNhanVien(Guid nhanvienId);
        Task<NhanVien_CMND> ThongTinNhanVienCMND(Guid nhanvienId);
        Task<NhanVien_GiayPhep> ThongTinNhanVienGiayPhep(Guid nhanvienId);
        Task<NhanVien_HopDong> ThongTinNhanVienHopDong(Guid nhanvienId, int? hopdongId, string tenLoaiHd);
        Task<NhanVien_ThoiViec> ThongTinNhanVienThoiViec(Guid nhanvienId, int hopdongId);
    }
}
