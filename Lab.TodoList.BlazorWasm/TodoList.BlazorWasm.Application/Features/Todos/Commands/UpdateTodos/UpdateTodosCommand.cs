using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Todos.Commands.UpdateTodos
{
    public class UpdateTodosCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Mode { get; set; }
        public int TypeId { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateTodosCommandHandler : IRequestHandler<UpdateTodosCommand, bool>
    {
        private readonly ITodosRepository _todoRepository;
        private readonly IMapper _mapper;

        public UpdateTodosCommandHandler(ITodosRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTodosCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<TodosList>(request);
            var result = await _todoRepository.UpdateTodoAsync(todo);
            return result != null ? true : false;
        }
    }
}
