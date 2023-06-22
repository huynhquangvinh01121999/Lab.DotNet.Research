using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView
{
    public class GetViecBenNgoaisNotHrViewQuery : IRequest<PagedResponse<IEnumerable<GetViecBenNgoaisNotHrViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }

    public class GetViecBenNgoaisNotHrViewQueryHandler : IRequestHandler<GetViecBenNgoaisNotHrViewQuery, PagedResponse<IEnumerable<GetViecBenNgoaisNotHrViewModel>>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;

        public GetViecBenNgoaisNotHrViewQueryHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetViecBenNgoaisNotHrViewModel>>> Handle(GetViecBenNgoaisNotHrViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var viecBenNgoai = await _viecBenNgoaiRepositoryAsync.S2_GetViecBenNgoaisNotHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.NhanVienId,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc,
                                                                                    request.TrangThai,
                                                                                    request.Keyword);
                var totalItems = await _viecBenNgoaiRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetViecBenNgoaisNotHrViewModel>>(viecBenNgoai, request.PageNumber, request.PageSize, totalItems);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
