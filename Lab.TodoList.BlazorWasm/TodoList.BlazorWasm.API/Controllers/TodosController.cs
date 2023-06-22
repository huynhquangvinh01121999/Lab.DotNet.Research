using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Application.Features.Todos.Commands.CreateTodos;
using TodoList.BlazorWasm.Application.Features.Todos.Commands.DeleteTodos;
using TodoList.BlazorWasm.Application.Features.Todos.Commands.UpdateTodos;
using TodoList.BlazorWasm.Application.Features.Todos.Queries.GetTodosByUserId;

namespace TodoList.BlazorWasm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _mediator.Send(new GetTodosQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodosById(int id)
        {
            return Ok(await _mediator.Send(new GetTodoByIdQuery { Id = id }));
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetTodosByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new GetTodosByUserIdQuery { UserId = userId }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodos([FromBody] CreateTodosCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodos([FromBody] UpdateTodosCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodos(int id)
        {
            return Ok(await _mediator.Send(new DeleteTodosCommand { Id = id }));
        }
    }
}
