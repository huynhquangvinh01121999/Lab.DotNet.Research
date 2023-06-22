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
    public class DisableKhoaDaoTaoByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableKhoaDaoTaoByIdCommandHandler : IRequestHandler<DisableKhoaDaoTaoByIdCommand, Response<int>>
        {
            private readonly IKhoaDaoTaoRepositoryAsync _khoadaotaoRepository;
            public DisableKhoaDaoTaoByIdCommandHandler(IKhoaDaoTaoRepositoryAsync khoadaotaoRepository)
            {
                _khoadaotaoRepository = khoadaotaoRepository;
            }
            public async Task<Response<int>> Handle(DisableKhoaDaoTaoByIdCommand command, CancellationToken cancellationToken)
            {
                var khoadaotao = await _khoadaotaoRepository.S2_GetByIdAsync(command.Id);

                if (khoadaotao == null)
                {
                    throw new ApiException($"KhoaDaoTao Not Found.");
                }
                else
                {
                    var result = await _khoadaotaoRepository.DisableAsync(khoadaotao);

                    if (result)
                    {
                        return new Response<int>(khoadaotao.Id);
                    }
                    else
                    {
                        throw new ApiException($"KhoaDaoTao is Using, Cannot Delete.");
                    }
                    //khoadaotao.Deleted = true;

                    //await _khoadaotaoRepository.UpdateAsync(khoadaotao);
                    //        return new Response<int>(khoadaotao.Id);
                }
            }
        }
    }
}
