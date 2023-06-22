using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.DeletePhuCaps
{
    public partial class DeletePhuCapByGuidCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCongTacByGuidCommandHandler : IRequestHandler<DeletePhuCapByGuidCommand, Response<string>>
    {
        public readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public DeleteCongTacByGuidCommandHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeletePhuCapByGuidCommand request, CancellationToken cancellationToken)
        {
            var pc = await _phuCapRepositoryAsync.S2_GetByGuidAsync(request.Id);
            if (pc is null)
                return new Response<string>($"PhuCap ID: {request.Id} was not found.");
            try
            {
                await _phuCapRepositoryAsync.DeleteAsync(pc);
                return new Response<string>(pc.Id.ToString(), null);
            }
            catch (Exception ex) {
                return new Response<string>($"An exception error has occurred: {ex.Message}");
            }
        }
    }
}
