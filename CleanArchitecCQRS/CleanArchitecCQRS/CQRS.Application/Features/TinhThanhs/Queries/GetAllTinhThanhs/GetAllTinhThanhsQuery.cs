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

namespace EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs
{
    public class GetAllTinhThanhsQuery : IRequest<PagedResponse<IEnumerable<GetAllTinhThanhsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllTinhThanhsQueryHandler : IRequestHandler<GetAllTinhThanhsQuery, PagedResponse<IEnumerable<GetAllTinhThanhsViewModel>>>
    {
        private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
        private readonly IMapper _mapper;
        public GetAllTinhThanhsQueryHandler(ITinhThanhRepositoryAsync tinhThanhRepository, IMapper mapper)
        {
            _tinhThanhRepository = tinhThanhRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllTinhThanhsViewModel>>> Handle(GetAllTinhThanhsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTinhThanhsParameter>(request);
            var tinhThanhs = await _tinhThanhRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var tinhThanhViewModel = _mapper.Map<IEnumerable<GetAllTinhThanhsViewModel>>(tinhThanhs);
            return new PagedResponse<IEnumerable<GetAllTinhThanhsViewModel>>(tinhThanhViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
