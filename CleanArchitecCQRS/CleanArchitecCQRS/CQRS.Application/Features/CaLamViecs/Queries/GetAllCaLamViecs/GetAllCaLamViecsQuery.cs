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

namespace EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetAllCaLamViecs
{
    public class GetAllCaLamViecsQuery : IRequest<PagedResponse<IEnumerable<GetAllCaLamViecsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllCaLamViecsQueryHandler : IRequestHandler<GetAllCaLamViecsQuery, PagedResponse<IEnumerable<GetAllCaLamViecsViewModel>>>
    {
        private readonly ICaLamViecRepositoryAsync _calamviecRepository;
        private readonly IMapper _mapper;
        public GetAllCaLamViecsQueryHandler(ICaLamViecRepositoryAsync calamviecRepository, IMapper mapper)
        {
            _calamviecRepository = calamviecRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllCaLamViecsViewModel>>> Handle(GetAllCaLamViecsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCaLamViecsParameter>(request);
            var calamviecs = await _calamviecRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var calamviecViewModel = _mapper.Map<IEnumerable<GetAllCaLamViecsViewModel>>(calamviecs);
            return new PagedResponse<IEnumerable<GetAllCaLamViecsViewModel>>(calamviecViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
