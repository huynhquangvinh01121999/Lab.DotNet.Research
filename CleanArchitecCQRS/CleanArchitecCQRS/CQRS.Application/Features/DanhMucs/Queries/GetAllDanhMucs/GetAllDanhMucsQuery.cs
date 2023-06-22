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
using EsuhaiHRM.Application.Features.DanhMucs.Queries.GetAllDanhMucs;

namespace EsuhaiHRM.Application.Features.DanhMucs.Queries.GetAllDanhMucs
{
    public class GetAllDanhMucsQuery : IRequest<PagedResponse<IEnumerable<GetAllDanhMucsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllDanhMucsQueryHandler : IRequestHandler<GetAllDanhMucsQuery, PagedResponse<IEnumerable<GetAllDanhMucsViewModel>>>
    {
        private readonly IDanhMucRepositoryAsync _danhMucRepository;
        private readonly IMapper _mapper;
        public GetAllDanhMucsQueryHandler(IDanhMucRepositoryAsync danhMucRepository, IMapper mapper)
        {
            _danhMucRepository = danhMucRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllDanhMucsViewModel>>> Handle(GetAllDanhMucsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllDanhMucsParameter>(request);
            var danhMucs = await _danhMucRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var danhMucViewModel = _mapper.Map<IEnumerable<GetAllDanhMucsViewModel>>(danhMucs);
            return new PagedResponse<IEnumerable<GetAllDanhMucsViewModel>>(danhMucViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
