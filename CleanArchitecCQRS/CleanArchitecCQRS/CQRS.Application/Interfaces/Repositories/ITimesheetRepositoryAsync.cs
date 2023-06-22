using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ITimesheetRepositoryAsync: IGenericRepositoryAsync<Timesheet>
    {
        Task<int> GetTotalItem();
        Task<Timesheet> S2_GetByGuidAsync(Guid Id);
        Task<GetDieuChinhTsDetailViewModel> S2_GetDieuChinhDetail(Guid Id);
        Task<IReadOnlyList<Timesheet>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
        //Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsByNhanVienIdAsync(Guid nhanvienId, int pageNumber, int pageSize);
        Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsInMonnth(Guid nhanvienId, int Thang, int Nam);

        Task<Timesheet> S2_GetTimesheetByDate(Guid nhanvienId, DateTime thoiGian);

        Task<IReadOnlyList<GetTimesheetsHrViewModel>> S2_GetTimesheetsHrView(int pageNumber,
                                                                            int pageSize,
                                                                            int phongId,
                                                                            int banId,
                                                                            string trangThai,
                                                                            string keyword,
                                                                            DateTime thoiGianBatDau,
                                                                            DateTime thoiGianKetThuc);

        Task<IReadOnlyList<GetTimesheetsNotHrViewModel>> S2_GetTimesheetsNotHrView(int pageNumber,
                                                                                    int pageSize,
                                                                                    Guid nhanVienId,
                                                                                    DateTime thoiGianBatDau,
                                                                                    DateTime thoiGianKetThuc,
                                                                                    string trangThai,
                                                                                    string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBan(int pageNumber,
                                                                                        int pageSize, 
                                                                                        DateTime thoiGian);

        Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBanHr(int pageNumber,
                                                                                        int pageSize, 
                                                                                        DateTime thoiGian,
                                                                                        int phongId, 
                                                                                        int banId, 
                                                                                        string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBanC1C2(int pageNumber,
                                                                                        int pageSize, 
                                                                                        DateTime thoiGian,
                                                                                        Guid nhanVienId,
                                                                                        string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>> SP_GetTimesheetsPhongBanV2Hr(int pageNumber,
                                                                                        int pageSize,
                                                                                        DateTime thoiGian,
                                                                                        int phongId,
                                                                                        int banId,
                                                                                        string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>> SP_GetTimesheetsPhongBanV2C1C2(int pageNumber,
                                                                                                int pageSize,
                                                                                                DateTime thoiGian,
                                                                                                Guid nhanVienId,
                                                                                                string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>> SP_GetTimesheetsPhongBanV3Hr(int pageNumber,
                                                                                        int pageSize,
                                                                                        DateTime thoiGian,
                                                                                        int phongId,
                                                                                        int banId,
                                                                                        string keyword);

        Task<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>> SP_GetTimesheetsPhongBanV3C1C2(int pageNumber,
                                                                                                int pageSize,
                                                                                                DateTime thoiGian,
                                                                                                Guid nhanVienId,
                                                                                                string keyword);
    }
}
