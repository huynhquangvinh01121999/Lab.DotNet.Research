using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;
using System;
using EsuhaiHRM.Application.Filters;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic
{
    public class GetAllNhanViensForHomeQuery : IRequest<PagedResponse<IEnumerable<GetAllNhanViensForHomeViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool BirthDay { get; set; }
    }
    public class GetAllNhanViensForHomeQueryHandler : IRequestHandler<GetAllNhanViensForHomeQuery, PagedResponse<IEnumerable<GetAllNhanViensForHomeViewModel>>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public GetAllNhanViensForHomeQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllNhanViensForHomeViewModel>>> Handle(GetAllNhanViensForHomeQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RequestParameter>(request);

            if (request.BirthDay)   
            {
                var nhanviens = await _nhanvienRepository.S2_GetListForBirthDay(validFilter.PageNumber
                                                                              , validFilter.PageSize
                                                                              , request.FromDate
                                                                              , request.ToDate);
                var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensForHomeViewModel>>(nhanviens);
                return new PagedResponse<IEnumerable<GetAllNhanViensForHomeViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize);
            }
            else
            {
                var nhanviens = await _nhanvienRepository.S2_GetListForThuViec(validFilter.PageNumber
                                                                             , validFilter.PageSize
                                                                             , request.FromDate
                                                                             , request.ToDate);
                var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensForHomeViewModel>>(nhanviens);
                return new PagedResponse<IEnumerable<GetAllNhanViensForHomeViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize);
            }  
        }
    }
}
