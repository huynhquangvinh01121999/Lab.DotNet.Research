using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView
{
    public class GetTangCasNotHrViewQuery : IRequest<PagedResponse<IEnumerable<GetTangCasNotHrViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTangCasNotViewHrQueryHandler : IRequestHandler<GetTangCasNotHrViewQuery, PagedResponse<IEnumerable<GetTangCasNotHrViewModel>>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public GetTangCasNotViewHrQueryHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }

        public async Task<PagedResponse<IEnumerable<GetTangCasNotHrViewModel>>> Handle(GetTangCasNotHrViewQuery request, CancellationToken cancellationToken)
        {
            var pcViewModel = await _tangCaRepository.S2_GetTangCasNotViewHr(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.NhanVienId,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc,
                                                                                    request.TrangThai,
                                                                                    request.Keyword);
            var totalItems = await _tangCaRepository.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTangCasNotHrViewModel>>(pcViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
