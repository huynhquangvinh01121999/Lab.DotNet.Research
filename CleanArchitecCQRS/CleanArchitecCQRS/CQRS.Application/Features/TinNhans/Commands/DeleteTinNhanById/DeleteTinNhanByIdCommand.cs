using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinNhans.Commands.DeleteTinNhanById
{
    public class DeleteTinNhanByIdCommand : IRequest<Response<TinNhan>>
    {
        public Guid Id { get; set; }
        public class DeleteTinNhanByIdCommandHandler : IRequestHandler<DeleteTinNhanByIdCommand, Response<TinNhan>>
        {
            private readonly ITinNhanRepositoryAsync _tinnhanRepository;
            public DeleteTinNhanByIdCommandHandler(ITinNhanRepositoryAsync tinnhanRepository)
            {
                _tinnhanRepository = tinnhanRepository;
            }
            public async Task<Response<TinNhan>> Handle(DeleteTinNhanByIdCommand command, CancellationToken cancellationToken)
            {
                var tinnhan = await _tinnhanRepository.S2_GetByIdAsync(command.Id);
                if (tinnhan == null) throw new ApiException($"TinNhan Not Found.");
                await _tinnhanRepository.DeleteAsync(tinnhan);
                return new Response<TinNhan>(tinnhan);
            }
        }
    }
}
