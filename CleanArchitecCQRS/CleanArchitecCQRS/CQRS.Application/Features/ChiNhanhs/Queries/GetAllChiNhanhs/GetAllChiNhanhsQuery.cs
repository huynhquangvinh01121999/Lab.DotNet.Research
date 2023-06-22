using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetAllChiNhanhs
{
    public class GetAllChiNhanhsQuery : IRequest<PagedResponse<IEnumerable<GetAllChiNhanhsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllChiNhanhsQueryHandler : IRequestHandler<GetAllChiNhanhsQuery, PagedResponse<IEnumerable<GetAllChiNhanhsViewModel>>>
    {
        private readonly IChiNhanhRepositoryAsync _chiNhanhRepository;
        private readonly IMapper _mapper;
        public GetAllChiNhanhsQueryHandler(IChiNhanhRepositoryAsync chiNhanhRepository, IMapper mapper)
        {
            _chiNhanhRepository = chiNhanhRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllChiNhanhsViewModel>>> Handle(GetAllChiNhanhsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllChiNhanhsParameter>(request);
            var chiNhanh = await _chiNhanhRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var chiNhanhViewModel = _mapper.Map<IEnumerable<GetAllChiNhanhsViewModel>>(chiNhanh);
            return new PagedResponse<IEnumerable<GetAllChiNhanhsViewModel>>(chiNhanhViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
