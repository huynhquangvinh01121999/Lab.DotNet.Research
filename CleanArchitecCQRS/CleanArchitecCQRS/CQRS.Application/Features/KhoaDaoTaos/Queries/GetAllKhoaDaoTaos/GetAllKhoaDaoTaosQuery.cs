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

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos
{
    public class GetAllKhoaDaoTaosQuery : IRequest<PagedResponse<IEnumerable<GetAllKhoaDaoTaosViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllKhoaDaoTaosQueryHandler : IRequestHandler<GetAllKhoaDaoTaosQuery, PagedResponse<IEnumerable<GetAllKhoaDaoTaosViewModel>>>
    {
        private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;
        private readonly IMapper _mapper;
        public GetAllKhoaDaoTaosQueryHandler(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository, IMapper mapper)
        {
            _khoaDaoTaoRepository = khoaDaoTaoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllKhoaDaoTaosViewModel>>> Handle(GetAllKhoaDaoTaosQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllKhoaDaoTaosParameter>(request);
            //var khoaDaotaos = await _khoaDaoTaoRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var khoaDaotaos = await _khoaDaoTaoRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var khoaDaotaoViewModel = _mapper.Map<IEnumerable<GetAllKhoaDaoTaosViewModel>>(khoaDaotaos);
            return new PagedResponse<IEnumerable<GetAllKhoaDaoTaosViewModel>>(khoaDaotaoViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
