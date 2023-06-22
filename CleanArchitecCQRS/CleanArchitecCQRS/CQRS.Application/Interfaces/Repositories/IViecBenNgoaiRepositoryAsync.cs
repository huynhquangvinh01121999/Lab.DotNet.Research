using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IViecBenNgoaiRepositoryAsync : IGenericRepositoryAsync<ViecBenNgoai>
    {
        Task<int> GetTotalItem();
        Task<ViecBenNgoai> S2_GetByGuidAsync(Guid Id);
        Task<GetDetailViecBenNgoaiViewModel> S2_GetDetailByGuidAsync(Guid Id, Guid nhanVienId);
        Task<IReadOnlyList<GetViecBenNgoaisNotHrViewModel>> S2_GetViecBenNgoaisNotHrView(int pageNumber, int pageSize, Guid nhanVienId, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, string trangThai, string keyword);
        Task<IReadOnlyList<GetViecBenNgoaiHrViewModel>> S2_GetViecBenNgoaisHrView(int pageNumber, int pageSize, int phongId, int banId, string trangThai, string keyword, DateTime thoiGianBatDau, DateTime thoiGianKetThuc);
    }
}
