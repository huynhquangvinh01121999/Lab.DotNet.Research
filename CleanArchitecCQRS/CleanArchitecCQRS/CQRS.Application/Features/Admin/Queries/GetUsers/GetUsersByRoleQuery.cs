using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Exceptions;

namespace EsuhaiHRM.Application.Features.Admin.Queries.GetUsers
{
    public class GetUsersByRoleQuery : IRequest<PagedResponse<IEnumerable<GetUsersByRoleViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string RoleName { get; set; }
    }
    public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, PagedResponse<IEnumerable<GetUsersByRoleViewModel>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;
        private readonly IMapper _mapper;
        public GetUsersByRoleQueryHandler(IAdminRepositoryAsync adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetUsersByRoleViewModel>>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RequestParameter>(request);
            if (request.RoleName == null)
            {
                throw new ApiException("RoleName cannot be null!");
            }
            var usersbyRole = await _adminRepository.GetUsersByRole(validFilter.PageNumber, validFilter.PageSize,request.RoleName);
            return new PagedResponse<IEnumerable<GetUsersByRoleViewModel>>(usersbyRole, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
