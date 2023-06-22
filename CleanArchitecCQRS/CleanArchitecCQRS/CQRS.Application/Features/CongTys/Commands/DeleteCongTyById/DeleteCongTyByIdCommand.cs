using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CongTys.Commands.DeleteCongTyById
{
    public class DeleteCongTyByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCongTyByIdCommandHandler : IRequestHandler<DeleteCongTyByIdCommand, Response<int>>
        {
            private readonly ICongTyRepositoryAsync _congtyRepository;
            public DeleteCongTyByIdCommandHandler(ICongTyRepositoryAsync congtyRepository)
            {
                _congtyRepository = congtyRepository;
            }
            public async Task<Response<int>> Handle(DeleteCongTyByIdCommand command, CancellationToken cancellationToken)
            {
                var congty = await _congtyRepository.S2_GetByIdAsync(command.Id);
                if (congty == null) throw new ApiException($"CongTy Not Found.");
                await _congtyRepository.DeleteAsync(congty);
                return new Response<int>(congty.Id);
            }
        }
    }
}
