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
    public class DisableChucVuByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableChucVuByIdCommandHandler : IRequestHandler<DisableChucVuByIdCommand, Response<int>>
        {
            private readonly IChucVuRepositoryAsync _chucvuRepository;
            public DisableChucVuByIdCommandHandler(IChucVuRepositoryAsync chucvuRepository)
            {
                _chucvuRepository = chucvuRepository;
            }
            public async Task<Response<int>> Handle(DisableChucVuByIdCommand command, CancellationToken cancellationToken)
            {
                var chucvu = await _chucvuRepository.S2_GetByIdAsync(command.Id);

                if (chucvu == null)
                {
                    throw new ApiException($"ChucVu Not Found.");
                }
                else
                {
                    var result = await _chucvuRepository.DisableAsync(chucvu);

                    if (result)
                    {
                        return new Response<int>(chucvu.Id);
                    }
                    else
                    {
                        throw new ApiException($"ChucVu is Using, Cannot Delete.");
                    }
                    //chucvu.Deleted = true;

                    //await _chucvuRepository.UpdateAsync(chucvu);
                    //return new Response<int>(chucvu.Id);
                }
            }
        }
    }
}
