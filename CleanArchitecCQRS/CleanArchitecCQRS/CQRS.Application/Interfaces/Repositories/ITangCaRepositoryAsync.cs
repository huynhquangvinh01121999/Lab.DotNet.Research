using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ITangCaRepositoryAsync: IGenericRepositoryAsync<TangCa>
    {
        Task<int> GetTotalItem();
        Task<TangCa> S2_GetByGuidAsync(Guid Id);
        Task<IReadOnlyList<TangCa>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<GetTangCasByNhanVienViewModel>> S2_GetTangCaByDate(Guid nhanvienId, DateTime ngay);
        Task<IReadOnlyList<GetTangCasNotHrViewModel>> S2_GetTangCasNotViewHr(int pageNumber,
                                                                                    int pageSize,
                                                                                    Guid nhanVienId,
                                                                                    DateTime thoiGianBatDau,
                                                                                    DateTime thoiGianKetThuc,
                                                                                    string trangThai,
                                                                                    string keyword);

        Task<IReadOnlyList<GetTangCasHrViewModel>> S2_GetTangCasViewHr(int pageNumber,
                                                                            int pageSize,
                                                                            int phongId,
                                                                            int banId,
                                                                            string trangThai,
                                                                            string keyword,
                                                                            DateTime thoiGianBatDau,
                                                                            DateTime thoiGianKetThuc);

        //Task<GetDieuChinhTsDetailViewModel> S2_GetDieuChinhDetail(Guid Id);
        //Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsByNhanVienIdAsync(Guid nhanvienId, int pageNumber, int pageSize);
    }
}
