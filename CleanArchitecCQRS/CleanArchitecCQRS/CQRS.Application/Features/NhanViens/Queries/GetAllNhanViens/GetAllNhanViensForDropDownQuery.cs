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

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensForDropDownQuery : IRequest<PagedResponse<IEnumerable<GetAllNhanViensForDropDownViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? PhongBanId { get; set; }

    }
    public class GetAllNhanViensForDropDownQueryHandler : IRequestHandler<GetAllNhanViensForDropDownQuery, PagedResponse<IEnumerable<GetAllNhanViensForDropDownViewModel>>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public GetAllNhanViensForDropDownQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllNhanViensForDropDownViewModel>>> Handle(GetAllNhanViensForDropDownQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllNhanViensForDropDownParameter>(request);
            
            //var nhanviens = await _nhanvienRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var nhanviens = await _nhanvienRepository.S2_GetListReponseAsync( validFilter.PageNumber
                                                                            , validFilter.PageSize
                                                                            , validFilter.PhongBanId);
            
            var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensForDropDownViewModel>>(nhanviens);
            
            return new PagedResponse<IEnumerable<GetAllNhanViensForDropDownViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
