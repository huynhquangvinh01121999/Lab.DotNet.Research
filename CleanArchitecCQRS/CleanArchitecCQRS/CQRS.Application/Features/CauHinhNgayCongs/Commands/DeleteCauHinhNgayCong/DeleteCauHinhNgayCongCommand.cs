using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.DeleteCauHinhNgayCong
{
    public class DeleteCauHinhNgayCongCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteCauHinhNgayCongCommandHandler : IRequestHandler<DeleteCauHinhNgayCongCommand, Response<string>>
    {
        private readonly ICauHinhNgayCongRepositoryAsync _cauHinhNgayCongRepositoryAsync;

        public DeleteCauHinhNgayCongCommandHandler(ICauHinhNgayCongRepositoryAsync cauHinhNgayCongRepositoryAsync)
        {
            _cauHinhNgayCongRepositoryAsync = cauHinhNgayCongRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeleteCauHinhNgayCongCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chnc = await _cauHinhNgayCongRepositoryAsync.S2_GetCauHinhNgayCongByIdAsync(request.Id);

                if (chnc == null)
                    return new Response<string>($"CHC002");

                await _cauHinhNgayCongRepositoryAsync.DeleteAsync(chnc);

                return new Response<string>(chnc.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
