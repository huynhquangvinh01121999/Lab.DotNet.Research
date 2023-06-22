using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView
{
    public class GetPhuCapsHrViewQuery : IRequest<PagedResponse<IEnumerable<GetPhuCapsHrViewModel>>>
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

    public class GetAllPhuCapsViewHRQueryHandler : IRequestHandler<GetPhuCapsHrViewQuery, PagedResponse<IEnumerable<GetPhuCapsHrViewModel>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public GetAllPhuCapsViewHRQueryHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetPhuCapsHrViewModel>>> Handle(GetPhuCapsHrViewQuery request, CancellationToken cancellationToken)
        {
            var pcViewModel = await _phuCapRepositoryAsync.S2_GetAllPhuCapHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.PhongId,
                                                                                    request.BanId,
                                                                                    request.TrangThai,
                                                                                    request.Keyword,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc);
            var totalItems = await _phuCapRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetPhuCapsHrViewModel>>(pcViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
