using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Todos.Queries.GetTodosByUserId
{
    public class GetTodosByUserIdQuery : IRequest<IReadOnlyList<GetTodosByUserIdQueryViewModel>>
    {
        public Guid UserId { get; set; }
    }

    public class GetTodosByUserIdQueryHandler : IRequestHandler<GetTodosByUserIdQuery, IReadOnlyList<GetTodosByUserIdQueryViewModel>>
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IMapper _mapper;

        public GetTodosByUserIdQueryHandler(ITodosRepository todosRepository, IMapper mapper)
        {
            _todosRepository = todosRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetTodosByUserIdQueryViewModel>> Handle(GetTodosByUserIdQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.GetTodosByUserIdAsync(request.UserId);

            var results = _mapper.Map<IReadOnlyList<GetTodosByUserIdQueryViewModel>>(todos);

            return results;
        }
    }
}
