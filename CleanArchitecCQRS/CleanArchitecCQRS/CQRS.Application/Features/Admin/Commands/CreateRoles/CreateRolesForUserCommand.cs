﻿using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Admin.Commands.CreateRoles
{
    public partial class CreateRolesForUserCommand : IRequest<Response<IList<string>>>
    {
        public string UserId { get; set; }
        public IList<string> RoleNames { get; set; }
    }
    public class CreateRolesForUserCommandHandler : IRequestHandler<CreateRolesForUserCommand, Response<IList<string>>>
    {
        private readonly IAdminRepositoryAsync _adminRepository;

        public CreateRolesForUserCommandHandler(IAdminRepositoryAsync adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Response<IList<string>>> Handle(CreateRolesForUserCommand request, CancellationToken cancellationToken)
        {
            var resultCreate = await _adminRepository.CreateRolesForUser(request.UserId,request.RoleNames);
            return new Response<IList<string>>(resultCreate);
        }
    }

}
