using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Admin.Commands.RemoveRoles
{
    public partial class RemoveOneRoleForUserCommand : ManageRoleParameter,IRequest<Response<IList<string>>>
    {
    }
    public class RemoveOneRoleForUserCommandHandler : IRequestHandler<RemoveOneRoleForUserCommand, Response<IList<string>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;

        public RemoveOneRoleForUserCommandHandler(IAdminRepositoryAsync adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Response<IList<string>>> Handle(RemoveOneRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var resultRemove = await _adminRepository.RemoveOneRoleForUser(request.UserId, request.RoleName);
            return new Response<IList<string>>(resultRemove);
        }
    }

}
