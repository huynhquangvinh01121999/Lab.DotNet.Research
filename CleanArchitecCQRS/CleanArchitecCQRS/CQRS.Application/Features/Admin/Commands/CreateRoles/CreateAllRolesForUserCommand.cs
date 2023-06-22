using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Admin.Commands.CreateRoles
{
    public partial class CreateAllRolesForUserCommand : IRequest<Response<IList<string>>>
    {
        public string UserId { get; set; }
    }
    public class CreateAllRolesForUserCommandHandler : IRequestHandler<CreateAllRolesForUserCommand, Response<IList<string>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;

        public CreateAllRolesForUserCommandHandler(IAdminRepositoryAsync adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Response<IList<string>>> Handle(CreateAllRolesForUserCommand request, CancellationToken cancellationToken)
        {
            var resultCreate = await _adminRepository.CreateAllRolesForUser(request.UserId);
            return new Response<IList<string>>(resultCreate);
        }
    }

}
