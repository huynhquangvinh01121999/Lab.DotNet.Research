using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.DeleteKhoaDaoTaoById
{
    public class DeleteKhoaDaoTaoByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteKhoaDaoTaoByIdCommandHandler : IRequestHandler<DeleteKhoaDaoTaoByIdCommand, Response<int>>
        {
            private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;
            public DeleteKhoaDaoTaoByIdCommandHandler(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository)
            {
                _khoaDaoTaoRepository = khoaDaoTaoRepository;
            }
            public async Task<Response<int>> Handle(DeleteKhoaDaoTaoByIdCommand command, CancellationToken cancellationToken)
            {
                var khoadaotao = await _khoaDaoTaoRepository.S2_GetByIdAsync(command.Id);
                if (khoadaotao == null) throw new ApiException($"KhoaDaoTao Not Found.");
                await _khoaDaoTaoRepository.DeleteAsync(khoadaotao);
                return new Response<int>(khoadaotao.Id);
            }
        }
    }
}
