using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuNhanVien;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ITongHopDuLieuRepositoryAsync : IGenericRepositoryAsync<TongHopDuLieu>
    {
        Task<int> GetTotalItem();
        Task<TongHopDuLieu> S2_GetByIdAsync(int Id);
        Task<IReadOnlyList<TongHopDuLieu>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<GetTongHopDuLieusByNhanVienViewModel>> S2_GetTimesheetsInMonth(Guid nhanvienId, int Thang, int Nam);
        Task<IReadOnlyList<GetTongHopDuLieuNhanVienVModel>> S2_GetTongHopDuLieuNhanVien(Guid nhanvienId, DateTime thoiGian);

        Task<IReadOnlyList<QuanLyNgayCongViewModel>> S2_QuanLyNgayCong(int pageNumber, int pageSize, DateTime thang, int phongId, string keyword, string orderBy);

        Task<int> S2_JobTongHopDuLieu(int thang, int nam);
        Task<IReadOnlyList<GetTongHopNghiViewModel>> S2_GetTongHopNghi(int thang, int nam);
        Task<IReadOnlyList<GetTongHopNgayCongViewModel>> S2_GetTongHopNgayCong(Guid nhanVienId, int thang, int nam);
    }
}
