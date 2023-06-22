using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong
{
    public class GetTongHopNgayCongQuery : IRequest<Response<IEnumerable<GetTongHopNgayCongViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }

    public class GetTongHopNgayCongQueryHandler : IRequestHandler<GetTongHopNgayCongQuery, Response<IEnumerable<GetTongHopNgayCongViewModel>>>
    {
        private readonly ITongHopDuLieuRepositoryAsync _tongHopDuLieuRepositoryAsync;

        public GetTongHopNgayCongQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepositoryAsync)
        {
            _tongHopDuLieuRepositoryAsync = tongHopDuLieuRepositoryAsync;
        }

        public async Task<Response<IEnumerable<GetTongHopNgayCongViewModel>>> Handle(GetTongHopNgayCongQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _tongHopDuLieuRepositoryAsync.S2_GetTongHopNgayCong(request.NhanVienId, request.Thang, request.Nam);
                var totalItems = await _tongHopDuLieuRepositoryAsync.GetTotalItem();

                return new Response<IEnumerable<GetTongHopNgayCongViewModel>>(results);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
