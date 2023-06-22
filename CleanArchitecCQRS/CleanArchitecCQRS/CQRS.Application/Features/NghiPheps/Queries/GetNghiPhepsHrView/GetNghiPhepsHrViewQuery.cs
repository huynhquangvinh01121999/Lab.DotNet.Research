using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView
{
    public class GetNghiPhepsHrViewQuery : IRequest<PagedResponse<IEnumerable<GetNghiPhepsHrViewModel>>>
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

    public class GetNghiPhepsHrViewQueryHandler : IRequestHandler<GetNghiPhepsHrViewQuery, PagedResponse<IEnumerable<GetNghiPhepsHrViewModel>>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;

        public GetNghiPhepsHrViewQueryHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetNghiPhepsHrViewModel>>> Handle(GetNghiPhepsHrViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var nghiphep = await _nghiPhepRepositoryAsync.S2_GetNghiPhepsHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.PhongId,
                                                                                    request.BanId,
                                                                                    request.TrangThai,
                                                                                    request.Keyword,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc);
                var totalItems = await _nghiPhepRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetNghiPhepsHrViewModel>>(nghiphep, request.PageNumber, request.PageSize, totalItems);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
