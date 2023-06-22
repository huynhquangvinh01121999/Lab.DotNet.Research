using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Admin.Commands.CreateRoles
{
    public partial class CreateOneRoleForUserCommand : ManageRoleParameter,IRequest<Response<string>>
    {
    }
    public class CreateOneRoleForUserCommandHandler : IRequestHandler<CreateOneRoleForUserCommand, Response<string>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;

        public CreateOneRoleForUserCommandHandler(IAdminRepositoryAsync adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Response<string>> Handle(CreateOneRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var resultCreate = await _adminRepository.CreateOneRoleForUser(request.UserId, request.RoleName);
            return new Response<string>(resultCreate,null);
        }
    }

}
