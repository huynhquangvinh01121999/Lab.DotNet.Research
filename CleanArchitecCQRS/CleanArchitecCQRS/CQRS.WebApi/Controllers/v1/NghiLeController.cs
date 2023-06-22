using AutoMapper;
using EsuhaiHRM.Application.Features.NghiLes.Commands.CreateNghiLe;
using EsuhaiHRM.Application.Features.NghiLes.Commands.DeleteNghiLe;
using EsuhaiHRM.Application.Features.NghiLes.Commands.UpdateNghiLe;
using EsuhaiHRM.Application.Features.NghiLes.Queries.GetNghiLes;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NghiLeController : BaseApiController
    {
        private readonly IMapper _mapper;

        public NghiLeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetNghiLesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(CreateNghiLeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(int id, UpdateNghiLeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteNghiLeCommand { Id = id}));
        }
    }
}
