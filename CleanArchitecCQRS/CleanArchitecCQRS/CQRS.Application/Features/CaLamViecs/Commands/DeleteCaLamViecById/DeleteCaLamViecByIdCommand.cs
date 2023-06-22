using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Commands.DeleteCaLamViecById
{
    public class DeleteCaLamViecByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCaLamViecByIdCommandHandler : IRequestHandler<DeleteCaLamViecByIdCommand, Response<int>>
        {
            private readonly ICaLamViecRepositoryAsync _calamviecRepository;
            public DeleteCaLamViecByIdCommandHandler(ICaLamViecRepositoryAsync calamviecRepository)
            {
                _calamviecRepository = calamviecRepository;
            }
            public async Task<Response<int>> Handle(DeleteCaLamViecByIdCommand command, CancellationToken cancellationToken)
            {
                var calamviec = await _calamviecRepository.S2_GetByIdAsync(command.Id);
                if (calamviec == null) throw new ApiException($"CaLamViec Not Found.");
                await _calamviecRepository.DeleteAsync(calamviec);
                return new Response<int>(calamviec.Id);
            }
        }
    }
}
