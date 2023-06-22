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
    public class GetRolesByUserQuery : IRequest<PagedResponse<IEnumerable<GetAllRolesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    public class GetRolesByUserQueryHandler : IRequestHandler<GetRolesByUserQuery, PagedResponse<IEnumerable<GetAllRolesViewModel>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;
        private readonly IMapper _mapper;
        public GetRolesByUserQueryHandler(IAdminRepositoryAsync adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllRolesViewModel>>> Handle(GetRolesByUserQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RequestParameter>(request);
            var roles = await _adminRepository.GetRolesByUser(validFilter.PageNumber, validFilter.PageSize,request.UserId);
            return new PagedResponse<IEnumerable<GetAllRolesViewModel>>(roles, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
