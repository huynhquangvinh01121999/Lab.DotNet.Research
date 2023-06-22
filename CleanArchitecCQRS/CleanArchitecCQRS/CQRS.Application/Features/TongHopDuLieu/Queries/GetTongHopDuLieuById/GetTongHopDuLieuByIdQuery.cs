using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuById
{
    public class GetTongHopDuLieuByIdQuery : IRequest<Response<EsuhaiHRM.Domain.Entities.TongHopDuLieu>>
    {
        public int Id { get; set; }
        public class GetTongHopDuLieuByIdQueryHandler : IRequestHandler<GetTongHopDuLieuByIdQuery, Response<EsuhaiHRM.Domain.Entities.TongHopDuLieu>>
        {
            private readonly ITongHopDuLieuRepositoryAsync _tonghopdulieuRepository;
            public GetTongHopDuLieuByIdQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepository)
            {
                _tonghopdulieuRepository = tongHopDuLieuRepository;
            }
            public async Task<Response<EsuhaiHRM.Domain.Entities.TongHopDuLieu>> Handle(GetTongHopDuLieuByIdQuery query, CancellationToken cancellationToken)
            {
                var TongHopDuLieu = await _tonghopdulieuRepository.S2_GetByIdAsync(query.Id);
                if (TongHopDuLieu == null) throw new ApiException($"TongHopDuLieu Not Found.");
                return new Response<EsuhaiHRM.Domain.Entities.TongHopDuLieu>(TongHopDuLieu);
            }
        }
    }
}
