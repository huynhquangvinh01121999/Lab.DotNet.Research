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

namespace EsuhaiHRM.Application.Features.PhongBans.Queries.GetPhongBanById
{
    public class GetPhongBanByIdQuery : IRequest<Response<PhongBan>>
    {
        public int Id { get; set; }
        public class GetPhongBanByIdQueryHandler : IRequestHandler<GetPhongBanByIdQuery, Response<PhongBan>>
        {
            private readonly IPhongBanRepositoryAsync _phongBanRepository;
            public GetPhongBanByIdQueryHandler(IPhongBanRepositoryAsync phongBanRepository)
            {
                _phongBanRepository = phongBanRepository;
            }
            public async Task<Response<PhongBan>> Handle(GetPhongBanByIdQuery query, CancellationToken cancellationToken)
            {
                var phongBan = await _phongBanRepository.S2_GetByIdAsync(query.Id);
                if (phongBan == null) throw new ApiException($"PhongBan Not Found.");
                return new Response<PhongBan>(phongBan);
            }
        }
    }
}
