using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Commands.DeleteCaLamViecById
{
    public class DisableCaLamViecByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableCaLamViecByIdCommandHandler : IRequestHandler<DisableCaLamViecByIdCommand, Response<int>>
        {
            private readonly ICaLamViecRepositoryAsync _calamviecRepository;
            public DisableCaLamViecByIdCommandHandler(ICaLamViecRepositoryAsync calamviecRepository)
            {
                _calamviecRepository = calamviecRepository;
            }
            public async Task<Response<int>> Handle(DisableCaLamViecByIdCommand command, CancellationToken cancellationToken)
            {
                var calamviec = await _calamviecRepository.S2_GetByIdAsync(command.Id);

                if (calamviec == null)
                {
                    throw new ApiException($"CaLamViec Not Found.");
                }
                else
                {
                    var result = await _calamviecRepository.DisableAsync(calamviec);

                    if (result)
                    {
                        return new Response<int>(calamviec.Id);
                    }
                    else
                    {
                        throw new ApiException($"CaLamViec is Using, Cannot Delete.");
                    }
                    //calamviec.Deleted = true;

                    //await _calamviecRepository.UpdateAsync(calamviec);
                    //        return new Response<int>(calamviec.Id);
                }
            }
        }
    }
}
