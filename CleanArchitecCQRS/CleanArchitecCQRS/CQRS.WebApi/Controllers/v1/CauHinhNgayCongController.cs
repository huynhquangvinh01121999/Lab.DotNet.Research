using AutoMapper;
using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.CreateCauHinhNgayCong;
using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.DeleteCauHinhNgayCong;
using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.UpdateCauHinhNgayCong;
using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Queries.GetCauHinhNgayCongs;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CauHinhNgayCongController : BaseApiController
    {
        private readonly IMapper _mapper;

        public CauHinhNgayCongController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetCauHinhNgayCongsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(CreateCauHinhNgayCongCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(int id, UpdateCauHinhNgayCongCommand command)
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
            return Ok(await Mediator.Send(new DeleteCauHinhNgayCongCommand { Id = id }));
        }
    }
}
