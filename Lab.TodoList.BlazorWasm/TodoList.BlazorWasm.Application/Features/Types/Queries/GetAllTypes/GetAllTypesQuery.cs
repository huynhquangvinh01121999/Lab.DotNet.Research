using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Interfaces;

namespace TodoList.BlazorWasm.Application.Features.Types.Queries.GetAllTypes
{
    public class GetAllTypesQuery : IRequest<IReadOnlyList<GetTypesQueryViewModel>>
    {
    }
    public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IReadOnlyList<GetTypesQueryViewModel>>
    {
        private readonly ITypesRepository _typesRepository;
        private readonly IMapper _mapper;

        public GetAllTypesQueryHandler(ITypesRepository typesRepository, IMapper mapper)
        {
            _typesRepository = typesRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetTypesQueryViewModel>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typesRepository.GetAllAsync();
            var resultVModel = _mapper.Map<IReadOnlyList<GetTypesQueryViewModel>>(types);
            return resultVModel;
        }
    }
}
