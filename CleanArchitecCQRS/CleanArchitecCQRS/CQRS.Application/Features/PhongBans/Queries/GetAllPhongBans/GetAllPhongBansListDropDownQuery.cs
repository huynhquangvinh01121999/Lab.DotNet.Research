using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans
{
    public class GetAllPhongBansListDropDownQuery : IRequest<PagedResponse<IEnumerable<GetAllPhongBansListDropDownViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPhongBansListDropDownQueryHandler : IRequestHandler<GetAllPhongBansListDropDownQuery, PagedResponse<IEnumerable<GetAllPhongBansListDropDownViewModel>>>
    {
        private readonly IPhongBanRepositoryAsync _phongBanRepository;
        private readonly IMapper _mapper;
        public GetAllPhongBansListDropDownQueryHandler(IPhongBanRepositoryAsync phongBanRepository, IMapper mapper)
        {
            _phongBanRepository = phongBanRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPhongBansListDropDownViewModel>>> Handle(GetAllPhongBansListDropDownQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllPhongBansListDropDownParameter>(request);
            var phongBans = await _phongBanRepository.S2_GetPagedReponseAsyncDropDown(validFilter.PageNumber, validFilter.PageSize);
            var phongBanViewModel = _mapper.Map<IEnumerable<GetAllPhongBansListDropDownViewModel>>(phongBans);
            return new PagedResponse<IEnumerable<GetAllPhongBansListDropDownViewModel>>(phongBanViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
