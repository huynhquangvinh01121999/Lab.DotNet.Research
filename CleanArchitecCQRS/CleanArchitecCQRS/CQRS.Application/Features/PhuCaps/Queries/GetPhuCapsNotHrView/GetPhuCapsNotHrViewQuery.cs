using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView
{
    public class GetPhuCapsNotHrViewQuery : IRequest<PagedResponse<IEnumerable<GetPhuCapsNotHrViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }

    public class GetAllPhuCapsNotViewHRQueryHandler : IRequestHandler<GetPhuCapsNotHrViewQuery, PagedResponse<IEnumerable<GetPhuCapsNotHrViewModel>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public GetAllPhuCapsNotViewHRQueryHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetPhuCapsNotHrViewModel>>> Handle(GetPhuCapsNotHrViewQuery request, CancellationToken cancellationToken)
        {
            var pcViewModel = await _phuCapRepositoryAsync.S2_GetAllPhuCapNotHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.NhanVienId,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc,
                                                                                    request.TrangThai,
                                                                                    request.Keyword);
            var totalItems = await _phuCapRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetPhuCapsNotHrViewModel>>(pcViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
