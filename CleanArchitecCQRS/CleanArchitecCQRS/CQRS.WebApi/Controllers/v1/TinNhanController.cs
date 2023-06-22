using System;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.TinNhans.Commands.CreateTinNhan;
using EsuhaiHRM.Application.Features.TinNhans.Commands.DeleteTinNhanById;
using EsuhaiHRM.Application.Features.TinNhans.Commands.UpdateTinNhan;
using EsuhaiHRM.Application.Features.TinNhans.Queries.GetAllTinNhans;
using EsuhaiHRM.Application.Features.TinNhans.Queries.GetTinNhanById;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TinNhanController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllTinNhansParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllTinNhansQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetTinNhanByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        //[Authorize]
        public async Task<IActionResult> Post(CreateTinNhanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(Guid id, UpdateTinNhanCommand command)
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
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteTinNhanByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(Guid id, DisableTinNhanByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
