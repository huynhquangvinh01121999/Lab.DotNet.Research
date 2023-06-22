using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi
{
    public class GetTongHopNghiQuery : IRequest<PagedResponse<IEnumerable<GetTongHopNghiViewModel>>>
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
    }

    public class GetTongHopNghiQueryHandler : IRequestHandler<GetTongHopNghiQuery, PagedResponse<IEnumerable<GetTongHopNghiViewModel>>>
    {
        private readonly ITongHopDuLieuRepositoryAsync _tongHopDuLieuRepositoryAsync;

        public GetTongHopNghiQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepositoryAsync)
        {
            _tongHopDuLieuRepositoryAsync = tongHopDuLieuRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTongHopNghiViewModel>>> Handle(GetTongHopNghiQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tonghopNgayCongs = await _tongHopDuLieuRepositoryAsync.S2_GetTongHopNghi(request.Thang, request.Nam);
                var totalItems = await _tongHopDuLieuRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetTongHopNghiViewModel>>(tonghopNgayCongs, 0, 1000, totalItems);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
