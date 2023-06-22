using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChucVus.Commands.DeleteChucVuById
{
    public class DeleteChucVuByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteChucVuByIdCommandHandler : IRequestHandler<DeleteChucVuByIdCommand, Response<int>>
        {
            private readonly IChucVuRepositoryAsync _chucvuRepository;
            public DeleteChucVuByIdCommandHandler(IChucVuRepositoryAsync chucvuRepository)
            {
                _chucvuRepository = chucvuRepository;
            }
            public async Task<Response<int>> Handle(DeleteChucVuByIdCommand command, CancellationToken cancellationToken)
            {
                var chucvu = await _chucvuRepository.S2_GetByIdAsync(command.Id);
                if (chucvu == null) throw new ApiException($"ChucVu Not Found.");
                await _chucvuRepository.DeleteAsync(chucvu);
                return new Response<int>(chucvu.Id);
            }
        }
    }
}
