using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.GroupMails.Commands.CreateGroupMail;
using EsuhaiHRM.Application.Features.GroupMails.Commands.DeleteGroupMailById;
using EsuhaiHRM.Application.Features.GroupMails.Commands.UpdateGroupMail;
using EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails;
using EsuhaiHRM.Application.Features.GroupMails.Queries.GetGroupMailById;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GroupMailController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles =Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllGroupMailsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllGroupMailsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetGroupMailByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        public async Task<IActionResult> Post(CreateGroupMailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(int id, UpdateGroupMailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.HRM_DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteGroupMailByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(int id, DisableGroupMailByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

    }
}
