using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView
{
    public class GetTangCasHrViewQuery : IRequest<PagedResponse<IEnumerable<GetTangCasHrViewModel>>>
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

    public class GetTangCasViewHrQueryHandler : IRequestHandler<GetTangCasHrViewQuery, PagedResponse<IEnumerable<GetTangCasHrViewModel>>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public GetTangCasViewHrQueryHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }

        public async Task<PagedResponse<IEnumerable<GetTangCasHrViewModel>>> Handle(GetTangCasHrViewQuery request, CancellationToken cancellationToken)
        {
            var tangcasVM = await _tangCaRepository.S2_GetTangCasViewHr(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.PhongId,
                                                                                    request.BanId,
                                                                                    request.TrangThai,
                                                                                    request.Keyword,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc);
            var totalItems = await _tangCaRepository.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTangCasHrViewModel>>(tangcasVM, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
