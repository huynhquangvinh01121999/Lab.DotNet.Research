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
using static EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens.GetAllNhanViensParameter;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensQuery : IRequest<PagedResponse<IEnumerable<GetAllNhanViensViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string SortParam { get; set; }
        public string FilterParams { get; set; }
        public string SearchParam { get; set; }

    }
    public class GetAllNhanViensQueryHandler : IRequestHandler<GetAllNhanViensQuery, PagedResponse<IEnumerable<GetAllNhanViensViewModel>>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public GetAllNhanViensQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllNhanViensViewModel>>> Handle(GetAllNhanViensQuery request, CancellationToken cancellationToken)
        {
            var totalItem = 0;
            var validFilter = _mapper.Map<GetAllNhanViensParameter>(request);
            
            //var nhanviens = await _nhanvienRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            
            var nhanviens = await _nhanvienRepository.S2_GetPagedReponseAsync(validFilter.PageNumber
                                                                            , validFilter.PageSize
                                                                            , validFilter.SortParam
                                                                            , validFilter.FilterParams
                                                                            , validFilter.SearchParam);
            //get count all iteam after GET request

            totalItem = await _nhanvienRepository.GetToTalItem();

            var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensViewModel>>(nhanviens);
            
            return new PagedResponse<IEnumerable<GetAllNhanViensViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize, totalItem);
        }
    }
}
