using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetDetailNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView;
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface INghiPhepRepositoryAsync : IGenericRepositoryAsync<NghiPhep>
    {
        Task<int> GetTotalItem();
        Task<NghiPhep> S2_GetByGuidAsync(Guid Id);
        Task<GetDetailNghiPhepViewModel> S2_GetNghiPhepDetailAsync(Guid id, Guid nhanVienId);
        Task<IReadOnlyList<GetNghiPhepsHrViewModel>> S2_GetNghiPhepsHrView(int pageNumber, int pageSize, int phongId, int banId, string trangThai, string keyword, DateTime thoiGianBatDau, DateTime thoiGianKetThuc);
        Task<IReadOnlyList<GetNghiPhepsNotHrViewModel>> S2_GetNghiPhepsNotHrView(int pageNumber,int pageSize,Guid nhanVienId,DateTime thoiGianBatDau,DateTime thoiGianKetThuc,string trangThai,string keyword);
    }
}
