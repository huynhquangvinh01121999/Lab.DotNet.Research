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

namespace EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans
{
    public class GetAllPhongBansQuery : IRequest<PagedResponse<IEnumerable<GetAllPhongBansViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool GetListDropDown { get; set; }
    }
    public class GetAllPhongBansQueryHandler : IRequestHandler<GetAllPhongBansQuery, PagedResponse<IEnumerable<GetAllPhongBansViewModel>>>
    {
        private readonly IPhongBanRepositoryAsync _phongBanRepository;
        private readonly IMapper _mapper;
        public GetAllPhongBansQueryHandler(IPhongBanRepositoryAsync phongBanRepository, IMapper mapper)
        {
            _phongBanRepository = phongBanRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPhongBansViewModel>>> Handle(GetAllPhongBansQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllPhongBansParameter>(request);
            //var phongBans = await _phongBanRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            if (validFilter.GetListDropDown == false)
            {
                var phongBans = await _phongBanRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
                var phongBanViewModel = _mapper.Map<IEnumerable<GetAllPhongBansViewModel>>(phongBans);
                return new PagedResponse<IEnumerable<GetAllPhongBansViewModel>>(phongBanViewModel, validFilter.PageNumber, validFilter.PageSize);
            }
            else
            {
                var phongBans = await _phongBanRepository.S2_GetPagedReponseAsyncDropDown(validFilter.PageNumber, validFilter.PageSize);
                var phongBanViewModel = _mapper.Map<IEnumerable<GetAllPhongBansViewModel>>(phongBans);
                return new PagedResponse<IEnumerable<GetAllPhongBansViewModel>>(phongBanViewModel, validFilter.PageNumber, validFilter.PageSize);
            }
        }
    }
}
