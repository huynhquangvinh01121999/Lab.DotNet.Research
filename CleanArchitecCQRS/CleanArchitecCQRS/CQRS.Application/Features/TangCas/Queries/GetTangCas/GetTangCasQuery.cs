using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCas
{
    public class GetTangCasQuery : IRequest<PagedResponse<IEnumerable<GetTangCasViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
    public class GetAllTangCasQueryHandler : IRequestHandler<GetTangCasQuery, PagedResponse<IEnumerable<GetTangCasViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITimesheetRepositoryAsync _timesheetRepository;

        public GetAllTangCasQueryHandler(ITimesheetRepositoryAsync timesheetRepository, IMapper mapper)
        {
            _timesheetRepository = timesheetRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetTangCasViewModel>>> Handle(GetTangCasQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;

            var validFilter = _mapper.Map<GetTangCasParameter>(request);
            var ts = await _timesheetRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);

            totalItems = await _timesheetRepository.GetTotalItem();
            var tsViewModel = _mapper.Map<IEnumerable<GetTangCasViewModel>>(ts);

            return new PagedResponse<IEnumerable<GetTangCasViewModel>>(tsViewModel, validFilter.PageNumber, validFilter.PageSize, totalItems);
        }
    }
}
