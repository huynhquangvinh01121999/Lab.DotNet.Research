using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Todos.Commands.CreateTodos
{
    public class CreateTodosCommand : IRequest<CreateTodosViewModel>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Mode { get; set; }    // public/private
        public Guid UserId { get; set; }
        public int TypeId { get; set; }
    }

    public class CreateTodosCommandHandler : IRequestHandler<CreateTodosCommand, CreateTodosViewModel>
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IMapper _mapper;

        public CreateTodosCommandHandler(ITodosRepository todosRepository, IMapper mapper)
        {
            _todosRepository = todosRepository;
            _mapper = mapper;
        }

        public async Task<CreateTodosViewModel> Handle(CreateTodosCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<TodosList>(request);

            var result = await _todosRepository.CreateTodoAsync(todo);

            return _mapper.Map<CreateTodosViewModel>(result);
        }
    }
}
