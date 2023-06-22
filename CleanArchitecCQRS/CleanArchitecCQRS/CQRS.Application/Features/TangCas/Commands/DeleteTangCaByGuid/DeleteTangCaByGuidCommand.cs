using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.DeleteTangCaByGuid
{
    public class DeleteTangCaByGuidCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteTangCaByGuidCommandHandler : IRequestHandler<DeleteTangCaByGuidCommand, Response<string>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;
        public DeleteTangCaByGuidCommandHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }
        public async Task<Response<string>> Handle(DeleteTangCaByGuidCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var ot = await _tangCaRepository.S2_GetByGuidAsync(command.Id);
                if (ot == null)
                    return new Response<string>($"TangCa Id {command.Id} was not found.");

                await _tangCaRepository.DeleteAsync(ot);
                return new Response<string>(ot.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
