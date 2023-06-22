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
    public class GetTodosQuery : IRequest<IReadOnlyList<GetTodosQueryViewModel>>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IReadOnlyList<GetTodosQueryViewModel>>
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(ITodosRepository todosRepository, IMapper mapper)
        {
            _todosRepository = todosRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetTodosQueryViewModel>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.GetTodosAsync();

            var results = _mapper.Map<IReadOnlyList<GetTodosQueryViewModel>>(todos);

            return results;
        }
    }
}
