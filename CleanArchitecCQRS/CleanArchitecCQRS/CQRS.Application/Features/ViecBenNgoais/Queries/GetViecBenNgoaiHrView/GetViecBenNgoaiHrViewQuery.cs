using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView
{
    public class GetViecBenNgoaiHrViewQuery : IRequest<PagedResponse<IEnumerable<GetViecBenNgoaiHrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }

    public class GetViecBenNgoaiHrViewQueryHandler : IRequestHandler<GetViecBenNgoaiHrViewQuery, PagedResponse<IEnumerable<GetViecBenNgoaiHrViewModel>>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;

        public GetViecBenNgoaiHrViewQueryHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetViecBenNgoaiHrViewModel>>> Handle(GetViecBenNgoaiHrViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var viecBenNgoais = await _viecBenNgoaiRepositoryAsync.S2_GetViecBenNgoaisHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.PhongId,
                                                                                    request.BanId,
                                                                                    request.TrangThai,
                                                                                    request.Keyword,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc);
                var totalItems = await _viecBenNgoaiRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetViecBenNgoaiHrViewModel>>(viecBenNgoais, request.PageNumber, request.PageSize, totalItems);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
