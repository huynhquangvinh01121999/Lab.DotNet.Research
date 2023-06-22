using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IPhuCapRepositoryAsync : IGenericRepositoryAsync<PhuCap>
    {
        Task<PhuCap> AddNewPhuCapsAsync(PhuCap entity);
        Task<PhuCap> AddNewPhuCapsAsync(PhuCap entity, string loaiPhuCapCode);
        Task UpdatePhuCapsAsync(PhuCap entity);
        Task UpdatePhuCapsAsync(PhuCap entity, string loaiPhuCapCode);
        Task<int> GetTotalItem();
        Task<PhuCap> S2_GetByGuidAsync(Guid Id);
        Task<IReadOnlyList<PhuCap>> S2_GetPagedReponseAsync(int pageNumber, int pageSize, int? phongId, int? banId);
        Task<IReadOnlyList<PhuCap>> S2_GetPhuCapByMonth(int pageNumber, int pageSize, Guid nhanvienId, DateTime month);

        Task<IReadOnlyList<GetPhuCapsHrViewModel>> S2_GetAllPhuCapHrView(int pageNumber, 
                                                                            int pageSize, 
                                                                            int phongId, 
                                                                            int banId, 
                                                                            string trangThai,
                                                                            string keyword,
                                                                            DateTime thoiGianBatDau,
                                                                            DateTime thoiGianKetThuc);
        Task<IReadOnlyList<GetPhuCapsNotHrViewModel>> S2_GetAllPhuCapNotHrView(int pageNumber, 
                                                                                    int pageSize, 
                                                                                    Guid nhanVienId, 
                                                                                    DateTime thoiGianBatDau, 
                                                                                    DateTime thoiGianKetThuc, 
                                                                                    string trangThai,
                                                                                    string keyword);

        Task<IReadOnlyList<GetPhuCapsByNhanVienViewModel>> S2_GetPhuCapByNhanVien(DateTime thang, Guid nhanvienId);

        //Task<GetDieuChinhTsDetailViewModel> S2_GetDieuChinhDetail(Guid Id);
        //Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsByNhanVienIdAsync(Guid nhanvienId, int pageNumber, int pageSize);
    }
}
