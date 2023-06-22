using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Admin.Commands.RemoveRoles
{
    public partial class RemoveAllRolesForUserCommand : IRequest<Response<IList<string>>>
    {
        public string UserId { get; set; }
    }
    public class RemoveAllRolesForUserCommandHandler : IRequestHandler<RemoveAllRolesForUserCommand, Response<IList<string>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;

        public RemoveAllRolesForUserCommandHandler(IAdminRepositoryAsync adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Response<IList<string>>> Handle(RemoveAllRolesForUserCommand request, CancellationToken cancellationToken)
        {
            var resultRemove = await _adminRepository.RemoveAllRolesForUser(request.UserId);

            return new Response<IList<string>>(resultRemove);
        }
    }

}
