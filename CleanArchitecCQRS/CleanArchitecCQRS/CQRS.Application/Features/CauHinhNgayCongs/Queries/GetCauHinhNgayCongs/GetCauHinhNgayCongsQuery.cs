using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Queries.GetCauHinhNgayCongs
{
    public class GetCauHinhNgayCongsQuery : IRequest<Response<IEnumerable<CauHinhNgayCong>>>
    {
        public int Nam { get; set; }
    }

    public class GetCauHinhNgayCongsQueryHandler : IRequestHandler<GetCauHinhNgayCongsQuery, Response<IEnumerable<CauHinhNgayCong>>>
    {
        private readonly ICauHinhNgayCongRepositoryAsync _cauHinhNgayCongRepositoryAsync;
        private readonly IMapper _mapper;

        public GetCauHinhNgayCongsQueryHandler(ICauHinhNgayCongRepositoryAsync cauHinhNgayCongRepositoryAsync, IMapper mapper)
        {
            _cauHinhNgayCongRepositoryAsync = cauHinhNgayCongRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CauHinhNgayCong>>> Handle(GetCauHinhNgayCongsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _cauHinhNgayCongRepositoryAsync.S2_GetCauHinhNgayCongsAsync(request.Nam);
                return new Response<IEnumerable<CauHinhNgayCong>>(results);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
