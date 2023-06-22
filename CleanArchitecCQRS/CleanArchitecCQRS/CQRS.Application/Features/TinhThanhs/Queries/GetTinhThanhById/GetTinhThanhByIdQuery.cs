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

namespace EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetTinhThanhById
{
    public class GetTinhThanhByIdQuery : IRequest<Response<TinhThanh>>
    {
        public int Id { get; set; }
        public class GetTinhThanhByIdQueryHandler : IRequestHandler<GetTinhThanhByIdQuery, Response<TinhThanh>>
        {
            private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
            public GetTinhThanhByIdQueryHandler(ITinhThanhRepositoryAsync tinhThanhRepository)
            {
                _tinhThanhRepository = tinhThanhRepository;
            }
            public async Task<Response<TinhThanh>> Handle(GetTinhThanhByIdQuery query, CancellationToken cancellationToken)
            {
                var tinhThanh = await _tinhThanhRepository.S2_GetByIdAsync(query.Id);
                if (tinhThanh == null) throw new ApiException($"TinhThanh Not Found.");
                return new Response<TinhThanh>(tinhThanh);
            }
        }
    }
}
