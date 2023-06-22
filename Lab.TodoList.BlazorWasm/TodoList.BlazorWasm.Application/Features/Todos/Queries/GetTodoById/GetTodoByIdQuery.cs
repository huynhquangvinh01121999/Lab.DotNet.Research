using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Todos.Queries.GetTodosByUserId
{
    public class GetTodoByIdQuery : IRequest<GetTodoByIdQueryViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTodosIdQueryHandler : IRequestHandler<GetTodoByIdQuery, GetTodoByIdQueryViewModel>
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IMapper _mapper;

        public GetTodosIdQueryHandler(ITodosRepository todosRepository, IMapper mapper)
        {
            _todosRepository = todosRepository;
            _mapper = mapper;
        }

        public async Task<GetTodoByIdQueryViewModel> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.GetByIdAsync(request.Id);

            var result = _mapper.Map<GetTodoByIdQueryViewModel>(todos);

            return result;
        }
    }
}
