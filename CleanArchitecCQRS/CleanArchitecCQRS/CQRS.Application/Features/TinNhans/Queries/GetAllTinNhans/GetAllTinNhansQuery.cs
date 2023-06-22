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
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Features.TinNhans.Queries.GetAllTinNhans
{
    public class GetAllTinNhansQuery : IRequest<PagedResponse<IEnumerable<GetAllTinNhansViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllTinNhansQueryHandler : IRequestHandler<GetAllTinNhansQuery, PagedResponse<IEnumerable<GetAllTinNhansViewModel>>>
    {
        private readonly ITinNhanRepositoryAsync _tinnhanRepository;
        private readonly IMapper _mapper;
        public GetAllTinNhansQueryHandler(ITinNhanRepositoryAsync tinnhanRepository, IMapper mapper)
        {
            _tinnhanRepository = tinnhanRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllTinNhansViewModel>>> Handle(GetAllTinNhansQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTinNhansParameter>(request);
            var tinnhans = await _tinnhanRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var tinnhanViewModel = _mapper.Map<IEnumerable<GetAllTinNhansViewModel>>(tinnhans);
            return new PagedResponse<IEnumerable<GetAllTinNhansViewModel>>(tinnhanViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
