using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.ChiNhanhs.Commands;
using EsuhaiHRM.Application.Features.ChiNhanhs.Commands.CreateChiNhanh;
using EsuhaiHRM.Application.Features.ChiNhanhs.Commands.DeleteChiNhanhById;
using EsuhaiHRM.Application.Features.ChiNhanhs.Commands.UpdateChiNhanh;
using EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetAllChiNhanhs;
using EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetChiNhanhById;
using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ChiNhanhController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllChiNhanhsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllChiNhanhsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetChiNhanhByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        public async Task<IActionResult> Post(CreateChiNhanhCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(Guid id, UpdateChiNhanhCommand command)
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
            return Ok(await Mediator.Send(new DeleteChiNhanhByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(Guid id, DisableChiNhanhByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
