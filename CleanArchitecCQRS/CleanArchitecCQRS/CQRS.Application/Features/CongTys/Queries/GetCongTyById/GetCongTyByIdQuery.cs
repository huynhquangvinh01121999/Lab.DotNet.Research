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

namespace EsuhaiHRM.Application.Features.CongTys.Queries.GetCongTyById
{
    public class GetCongTyByIdQuery : IRequest<Response<CongTy>>
    {
        public int Id { get; set; }
        public class GetCongTyByIdQueryHandler : IRequestHandler<GetCongTyByIdQuery, Response<CongTy>>
        {
            private readonly ICongTyRepositoryAsync _congtyRepository;
            public GetCongTyByIdQueryHandler(ICongTyRepositoryAsync congtyRepository)
            {
                _congtyRepository = congtyRepository;
            }
            public async Task<Response<CongTy>> Handle(GetCongTyByIdQuery query, CancellationToken cancellationToken)
            {
                var congty = await _congtyRepository.S2_GetByIdAsync(query.Id);
                if (congty == null) throw new ApiException($"CongTy Not Found.");
                return new Response<CongTy>(congty);
            }
        }
    }
}
