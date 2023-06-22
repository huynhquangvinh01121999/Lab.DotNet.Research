using EsuhaiHRM.Application.Features.Admin.Commands.CreateRoles;
using EsuhaiHRM.Application.Features.Admin.Commands.RemoveRoles;
using EsuhaiHRM.Application.Features.Admin.Queries.GetRoles;
using EsuhaiHRM.Application.Features.Admin.Queries.GetUsers;
using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseApiController
    {
        [HttpPost("CreateAllRoles")]
        [Authorize(Roles = Role.SUPERADMIN_ADMIN)]
        public async Task<ActionResult> CreateAllRolesForUser(CreateAllRolesForUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("RemoveAllRoles")]
        [Authorize(Roles = Role.SUPERADMIN_ADMIN)]
        public async Task<ActionResult> RemoveAllRolesForUser(RemoveAllRolesForUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateOneRole")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> CreateOneRoleForUser(CreateOneRoleForUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("RemoveOneRole")]
        [Authorize(Roles = Role.SUPERADMIN_ADMIN)]
        public async Task<ActionResult> RemoveOneRoleForUser(RemoveOneRoleForUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> GetAllRoles([FromQuery]RequestParameter requestParameter)
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery() { PageNumber = requestParameter.PageNumber
                                                                 , PageSize = requestParameter.PageSize}));
        }

        [HttpGet("GetUsersByRole")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> GetUsersByRole([FromQuery]RequestParameter requestParameter, string roleName)
        {
            return Ok(await Mediator.Send(new GetUsersByRoleQuery() { PageNumber = requestParameter.PageNumber,
                                                                      PageSize = requestParameter.PageSize,
                                                                      RoleName = roleName
            }));
        }

        [HttpGet("GetRolesByUser")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> GetRolesByUser([FromQuery]RequestParameter requestParameter, string userId)
        {
            return Ok(await Mediator.Send(new GetRolesByUserQuery()
            {
                PageNumber = requestParameter.PageNumber,
                PageSize = requestParameter.PageSize,
                UserId = userId
            }));
        }

        [HttpPost("CreateRolesForUser")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> CreateRolesForUser(string userId, IList<string> roleNames)
        {
            return Ok(await Mediator.Send(new CreateRolesForUserCommand()
            {
                UserId = userId,
                RoleNames = roleNames
            }));
        }

    }
}