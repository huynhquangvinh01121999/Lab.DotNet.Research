using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhongBans.Commands.DeletePhongBanById
{
    public class DeletePhongBanByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeletePhongBanByIdCommandHandler : IRequestHandler<DeletePhongBanByIdCommand, Response<int>>
        {
            private readonly IPhongBanRepositoryAsync _phongBanRepository;
            public DeletePhongBanByIdCommandHandler(IPhongBanRepositoryAsync phongBanRepository)
            {
                _phongBanRepository = phongBanRepository;
            }
            public async Task<Response<int>> Handle(DeletePhongBanByIdCommand command, CancellationToken cancellationToken)
            {
                var phongBan = await _phongBanRepository.S2_GetByIdAsync(command.Id);
                if (phongBan == null) throw new ApiException($"PhongBan Not Found.");
                await _phongBanRepository.DeleteAsync(phongBan);
                return new Response<int>(phongBan.Id);
            }
        }
    }
}
