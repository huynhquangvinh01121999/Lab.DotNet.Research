using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinNhans.Commands.DeleteTinNhanById
{
    public class DisableTinNhanByIdCommand : IRequest<Response<TinNhan>>
    {
        public Guid Id { get; set; }
        public class DisableTinNhanByIdCommandHandler : IRequestHandler<DisableTinNhanByIdCommand, Response<TinNhan>>
        {
            private readonly ITinNhanRepositoryAsync _tinnhanRepository;
            public DisableTinNhanByIdCommandHandler(ITinNhanRepositoryAsync tinnhanRepository)
            {
                _tinnhanRepository = tinnhanRepository;
            }
            public async Task<Response<TinNhan>> Handle(DisableTinNhanByIdCommand command, CancellationToken cancellationToken)
            {
                var tinnhan = await _tinnhanRepository.S2_GetByIdAsync(command.Id);

                if (tinnhan == null)
                {
                    throw new ApiException($"TinNhan Not Found.");
                }
                else
                {
                    tinnhan.Deleted = true;

                    await _tinnhanRepository.UpdateAsync(tinnhan);
                    return new Response<TinNhan>(tinnhan);
                }
            }
        }
    }
}
