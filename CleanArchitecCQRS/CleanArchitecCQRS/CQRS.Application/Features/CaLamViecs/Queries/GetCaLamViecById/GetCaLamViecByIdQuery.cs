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

namespace EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetCaLamViecById
{
    public class GetCaLamViecByIdQuery : IRequest<Response<CaLamViec>>
    {
        public int Id { get; set; }
        public class GetCaLamViecByIdQueryHandler : IRequestHandler<GetCaLamViecByIdQuery, Response<CaLamViec>>
        {
            private readonly ICaLamViecRepositoryAsync _calamviecRepository;
            public GetCaLamViecByIdQueryHandler(ICaLamViecRepositoryAsync calamviecRepository)
            {
                _calamviecRepository = calamviecRepository;
            }
            public async Task<Response<CaLamViec>> Handle(GetCaLamViecByIdQuery query, CancellationToken cancellationToken)
            {
                var calamviec = await _calamviecRepository.S2_GetByIdAsync(query.Id);
                if (calamviec == null) throw new ApiException($"CaLamViec Not Found.");
                return new Response<CaLamViec>(calamviec);
            }
        }
    }
}
