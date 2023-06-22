using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Filters;

namespace EsuhaiHRM.Application.Features.Admin.Queries.GetRoles
{
    public class GetAllRolesQuery : IRequest<PagedResponse<IEnumerable<GetAllRolesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllBaoHiemsQueryHandler : IRequestHandler<GetAllRolesQuery, PagedResponse<IEnumerable<GetAllRolesViewModel>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;
        private readonly IMapper _mapper;
        public GetAllBaoHiemsQueryHandler(IAdminRepositoryAsync adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllRolesViewModel>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RequestParameter>(request);
            var roles = await _adminRepository.GetAllRoles(validFilter.PageNumber, validFilter.PageSize);
            return new PagedResponse<IEnumerable<GetAllRolesViewModel>>(roles, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
