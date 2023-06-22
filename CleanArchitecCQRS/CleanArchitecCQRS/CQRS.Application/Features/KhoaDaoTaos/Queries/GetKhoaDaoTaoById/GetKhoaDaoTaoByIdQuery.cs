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

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetKhoaDaoTaoById
{
    public class GetKhoaDaoTaoByIdQuery : IRequest<Response<KhoaDaoTao>>
    {
        public int Id { get; set; }
        public class GetKhoaDaoTaoByIdQueryHandler : IRequestHandler<GetKhoaDaoTaoByIdQuery, Response<KhoaDaoTao>>
        {
            private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;
            public GetKhoaDaoTaoByIdQueryHandler(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository)
            {
                _khoaDaoTaoRepository = khoaDaoTaoRepository;
            }
            public async Task<Response<KhoaDaoTao>> Handle(GetKhoaDaoTaoByIdQuery query, CancellationToken cancellationToken)
            {
                var khoaDaotao = await _khoaDaoTaoRepository.S2_GetByIdAsync(query.Id);
                if (khoaDaotao == null) throw new ApiException($"KhoaDaoTao Not Found.");
                return new Response<KhoaDaoTao>(khoaDaotao);
            }
        }
    }
}
