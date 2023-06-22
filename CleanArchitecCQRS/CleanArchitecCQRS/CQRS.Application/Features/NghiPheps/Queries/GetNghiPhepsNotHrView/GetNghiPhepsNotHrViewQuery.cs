using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView
{
    public class GetNghiPhepsNotHrViewQuery : IRequest<PagedResponse<IEnumerable<GetNghiPhepsNotHrViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }

    public class GetNghiPhepsNotHrViewQueryHandler : IRequestHandler<GetNghiPhepsNotHrViewQuery, PagedResponse<IEnumerable<GetNghiPhepsNotHrViewModel>>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;

        public GetNghiPhepsNotHrViewQueryHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetNghiPhepsNotHrViewModel>>> Handle(GetNghiPhepsNotHrViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var nghipheps = await _nghiPhepRepositoryAsync.S2_GetNghiPhepsNotHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.NhanVienId,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc,
                                                                                    request.TrangThai,
                                                                                    request.Keyword);
                var totalItems = await _nghiPhepRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetNghiPhepsNotHrViewModel>>(nghipheps, request.PageNumber, request.PageSize, totalItems);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
