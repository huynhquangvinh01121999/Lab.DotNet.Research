using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.CreateKhoaDaoTao;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.DeleteKhoaDaoTaoById;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.UpdateKhoaDaoTao;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetKhoaDaoTaoById;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class KhoaDaoTaoController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllKhoaDaoTaosParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllKhoaDaoTaosQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetKhoaDaoTaoByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.HRM_ADD)]
        public async Task<IActionResult> Post(CreateKhoaDaoTaoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Put(int id, UpdateKhoaDaoTaoCommand command)
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
            return Ok(await Mediator.Send(new DeleteKhoaDaoTaoByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.HRM_EDIT)]
        public async Task<IActionResult> Disable(int id, DisableKhoaDaoTaoByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
