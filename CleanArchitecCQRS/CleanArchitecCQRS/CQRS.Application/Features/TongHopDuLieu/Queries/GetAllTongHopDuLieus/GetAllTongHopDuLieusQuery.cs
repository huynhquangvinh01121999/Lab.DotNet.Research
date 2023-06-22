
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetAllTongHopDuLieus
{
    public class GetAllTongHopDuLieusQuery : IRequest<PagedResponse<IEnumerable<GetAllTongHopDuLieusViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
    public class GetAllTongHopDuLieusQueryHandler : IRequestHandler<GetAllTongHopDuLieusQuery, PagedResponse<IEnumerable<GetAllTongHopDuLieusViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITongHopDuLieuRepositoryAsync _tonghopduulieuRepository;

        public GetAllTongHopDuLieusQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepository, IMapper mapper)
        {
            _tonghopduulieuRepository = tongHopDuLieuRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllTongHopDuLieusViewModel>>> Handle(GetAllTongHopDuLieusQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;

            var validFilter = _mapper.Map<GetAllTongHopDuLieusParameter>(request);
            var ts = await _tonghopduulieuRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);

            totalItems = await _tonghopduulieuRepository.GetTotalItem();
            var tsViewModel = _mapper.Map<IEnumerable<GetAllTongHopDuLieusViewModel>>(ts);

            return new PagedResponse<IEnumerable<GetAllTongHopDuLieusViewModel>>(tsViewModel, validFilter.PageNumber, validFilter.PageSize, totalItems);
        }
    }
}
