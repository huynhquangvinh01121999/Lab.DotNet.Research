using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetChiNhanhById
{
    public class GetChiNhanhByIdQuery : IRequest<Response<ChiNhanh>>
    {
        public Guid Id { get; set; }
        public class GetChiNhanhByIdQueryHandler : IRequestHandler<GetChiNhanhByIdQuery, Response<ChiNhanh>>
        {
            private readonly IChiNhanhRepositoryAsync _chiNhanhRepository;
            public GetChiNhanhByIdQueryHandler(IChiNhanhRepositoryAsync chiNhanhRepository)
            {
                _chiNhanhRepository = chiNhanhRepository;
            }
            public async Task<Response<ChiNhanh>> Handle(GetChiNhanhByIdQuery query, CancellationToken cancellationToken)
            {
                var chiNhanh = await _chiNhanhRepository.S2_GetByIdAsync(query.Id);
                if (chiNhanh == null) throw new ApiException($"ChiNhanh Not Found.");
                return new Response<ChiNhanh>(chiNhanh);
            }
        }
    }
}
