using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.LoaiPhuCaps.Queries.GetAllLoaiPhuCap
{
    public class GetAllLoaiPhuCapQuery : IRequest<Response<IEnumerable<GetAllLoaiPhuCapViewModel>>>
    {
    }

    public class GetAllLoaiPhuCapQueryHandler : IRequestHandler<GetAllLoaiPhuCapQuery, Response<IEnumerable<GetAllLoaiPhuCapViewModel>>>
    {
        private readonly ILoaiPhuCapRepositoryAsync _loaiPhuCapRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllLoaiPhuCapQueryHandler(ILoaiPhuCapRepositoryAsync loaiPhuCapRepositoryAsync, IMapper mapper)
        {
            _loaiPhuCapRepositoryAsync = loaiPhuCapRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllLoaiPhuCapViewModel>>> Handle(GetAllLoaiPhuCapQuery request, CancellationToken cancellationToken)
        {
            var loaiPhuCaps = await _loaiPhuCapRepositoryAsync.S2_GetAllLoaiPhuCap();
            var resultVModel = _mapper.Map<IEnumerable<GetAllLoaiPhuCapViewModel>>(loaiPhuCaps);
            return new Response<IEnumerable<GetAllLoaiPhuCapViewModel>>(resultVModel);
        }
    }
}
