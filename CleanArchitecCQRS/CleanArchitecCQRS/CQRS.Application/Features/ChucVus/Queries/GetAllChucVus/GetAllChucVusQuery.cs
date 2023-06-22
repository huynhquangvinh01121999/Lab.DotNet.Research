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

namespace EsuhaiHRM.Application.Features.ChucVus.Queries.GetAllChucVus
{
    public class GetAllChucVusQuery : IRequest<PagedResponse<IEnumerable<GetAllChucVusViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllChucVusQueryHandler : IRequestHandler<GetAllChucVusQuery, PagedResponse<IEnumerable<GetAllChucVusViewModel>>>
    {
        private readonly IChucVuRepositoryAsync _chucvuRepository;
        private readonly IMapper _mapper;
        public GetAllChucVusQueryHandler(IChucVuRepositoryAsync chucvuRepository, IMapper mapper)
        {
            _chucvuRepository = chucvuRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllChucVusViewModel>>> Handle(GetAllChucVusQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllChucVusParameter>(request);
            //var chucvus = await _chucvuRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var chucvus = await _chucvuRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var chucvuViewModel = _mapper.Map<IEnumerable<GetAllChucVusViewModel>>(chucvus);
            return new PagedResponse<IEnumerable<GetAllChucVusViewModel>>(chucvuViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
