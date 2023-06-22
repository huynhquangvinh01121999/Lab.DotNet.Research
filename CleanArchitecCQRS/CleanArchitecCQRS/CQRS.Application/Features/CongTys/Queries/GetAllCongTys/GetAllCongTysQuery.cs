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

namespace EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys
{
    public class GetAllCongTysQuery : IRequest<PagedResponse<IEnumerable<GetAllCongTysViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
    public class GetAllCongTysQueryHandler : IRequestHandler<GetAllCongTysQuery, PagedResponse<IEnumerable<GetAllCongTysViewModel>>>
    {
        private readonly ICongTyRepositoryAsync _congtyRepository;
        private readonly IMapper _mapper;
        public GetAllCongTysQueryHandler(ICongTyRepositoryAsync congtyRepository, IMapper mapper)
        {
            _congtyRepository = congtyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllCongTysViewModel>>> Handle(GetAllCongTysQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;

            var validFilter = _mapper.Map<GetAllCongTysParameter>(request);

            //var congtys = await _congtyRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            
            var congtys = await _congtyRepository.S2_GetPagedReponseAsyncWithSearch(validFilter.PageNumber, validFilter.PageSize, validFilter.SearchValue);
            
            //get count all iteam after GET request
            
            totalItems = _congtyRepository.GetToTalItemReponse();
            
            var congtyViewModel = _mapper.Map<IEnumerable<GetAllCongTysViewModel>>(congtys);

            return new PagedResponse<IEnumerable<GetAllCongTysViewModel>>(congtyViewModel, validFilter.PageNumber, validFilter.PageSize, totalItems);
        }
    }
}
