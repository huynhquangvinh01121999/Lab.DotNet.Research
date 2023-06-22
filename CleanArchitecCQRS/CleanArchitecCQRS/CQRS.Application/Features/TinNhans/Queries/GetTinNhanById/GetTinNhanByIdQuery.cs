using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinNhans.Queries.GetTinNhanById
{
    public class GetTinNhanByIdQuery : IRequest<Response<TinNhan>>
    {
        public Guid Id { get; set; }
        public class GetTinNhanByIdQueryHandler : IRequestHandler<GetTinNhanByIdQuery, Response<TinNhan>>
        {
            private readonly ITinNhanRepositoryAsync _tinnhanRepository;
            public GetTinNhanByIdQueryHandler(ITinNhanRepositoryAsync tinnhanRepository)
            {
                _tinnhanRepository = tinnhanRepository;
            }
            public async Task<Response<TinNhan>> Handle(GetTinNhanByIdQuery query, CancellationToken cancellationToken)
            {
                var tinnhan = await _tinnhanRepository.S2_GetByIdAsync(query.Id);
                if (tinnhan == null) throw new ApiException($"TinNhan Not Found.");
                return new Response<TinNhan>(tinnhan);
            }
        }
    }
}
