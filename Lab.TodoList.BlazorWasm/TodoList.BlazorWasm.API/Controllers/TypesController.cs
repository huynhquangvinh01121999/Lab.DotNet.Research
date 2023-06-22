using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Application.Features.Types.Queries.GetAllTypes;

namespace TodoList.BlazorWasm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypes()
        {
            return Ok(await _mediator.Send(new GetAllTypesQuery()));
        }
    }
}
