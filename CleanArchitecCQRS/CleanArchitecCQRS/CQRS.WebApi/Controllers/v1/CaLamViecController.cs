using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.CaLamViecs.Commands.CreateCaLamViec;
using EsuhaiHRM.Application.Features.CaLamViecs.Commands.DeleteCaLamViecById;
using EsuhaiHRM.Application.Features.CaLamViecs.Commands.UpdateCaLamViec;
using EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetAllCaLamViecs;
using EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetCaLamViecById;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CaLamViecController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllCaLamViecsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllCaLamViecsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCaLamViecByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        public async Task<IActionResult> Post(CreateCaLamViecCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(int id, UpdateCaLamViecCommand command)
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
            return Ok(await Mediator.Send(new DeleteCaLamViecByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(int id, DisableCaLamViecByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
