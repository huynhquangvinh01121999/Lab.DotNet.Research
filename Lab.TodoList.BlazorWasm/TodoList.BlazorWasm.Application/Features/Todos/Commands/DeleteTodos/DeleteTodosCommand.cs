using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Todos.Commands.DeleteTodos
{
    public class DeleteTodosCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteTodosCommandHandler : IRequestHandler<DeleteTodosCommand, bool>
    {
        private readonly ITodosRepository _todosRepository;

        public DeleteTodosCommandHandler(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<bool> Handle(DeleteTodosCommand request, CancellationToken cancellationToken)
        {
            return await _todosRepository.DeleteTodoAsync(request.Id);
        }
    }
}
