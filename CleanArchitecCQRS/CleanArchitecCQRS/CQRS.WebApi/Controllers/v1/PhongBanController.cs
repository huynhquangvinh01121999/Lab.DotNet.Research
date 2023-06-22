using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.PhongBans.Commands.CreatePhongBan;
using EsuhaiHRM.Application.Features.PhongBans.Commands.DeletePhongBanById;
using EsuhaiHRM.Application.Features.PhongBans.Commands.UpdatePhongBan;
using EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans;
using EsuhaiHRM.Application.Features.PhongBans.Queries.GetPhongBanById;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PhongBanController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllPhongBansParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllPhongBansQuery() { 
                                          PageSize = filter.PageSize
                                        , PageNumber = filter.PageNumber
                                        , GetListDropDown = filter.GetListDropDown }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPhongBanByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        public async Task<IActionResult> Post(CreatePhongBanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(int id, UpdatePhongBanCommand command)
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
            return Ok(await Mediator.Send(new DeletePhongBanByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(int id, DisablePhongBanByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
