using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiLes.Queries.GetNghiLes
{
    public class GetNghiLesQuery : IRequest<Response<IEnumerable<NghiLe>>>
    {
        public int Nam { get; set; }
    }

    public class GetNghiLesQueryHandler : IRequestHandler<GetNghiLesQuery, Response<IEnumerable<NghiLe>>>
    {
        private readonly INghiLeRepositoryAsync _nghiLeRepositoryAsync;

        public GetNghiLesQueryHandler(INghiLeRepositoryAsync nghiLeRepositoryAsync)
        {
            _nghiLeRepositoryAsync = nghiLeRepositoryAsync;
        }

        public async Task<Response<IEnumerable<NghiLe>>> Handle(GetNghiLesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _nghiLeRepositoryAsync.S2_GetNghiLesAsync(request.Nam);
                return new Response<IEnumerable<NghiLe>>(results);
            }catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
