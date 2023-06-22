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

namespace EsuhaiHRM.Application.Features.ChucVus.Queries.GetChucVuById
{
    public class GetChucVuByIdQuery : IRequest<Response<ChucVu>>
    {
        public int Id { get; set; }
        public class GetChucVuByIdQueryHandler : IRequestHandler<GetChucVuByIdQuery, Response<ChucVu>>
        {
            private readonly IChucVuRepositoryAsync _chucvuRepository;
            public GetChucVuByIdQueryHandler(IChucVuRepositoryAsync chucvuRepository)
            {
                _chucvuRepository = chucvuRepository;
            }
            public async Task<Response<ChucVu>> Handle(GetChucVuByIdQuery query, CancellationToken cancellationToken)
            {
                var chucvu = await _chucvuRepository.S2_GetByIdAsync(query.Id);
                if (chucvu == null) throw new ApiException($"ChucVu Not Found.");
                return new Response<ChucVu>(chucvu);
            }
        }
    }
}
