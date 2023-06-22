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
    public class DisablePhongBanByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisablePhongBanByIdCommandHandler : IRequestHandler<DisablePhongBanByIdCommand, Response<int>>
        {
            private readonly IPhongBanRepositoryAsync _phongbanRepository;
            public DisablePhongBanByIdCommandHandler(IPhongBanRepositoryAsync phongbanRepository)
            {
                _phongbanRepository = phongbanRepository;
            }
            public async Task<Response<int>> Handle(DisablePhongBanByIdCommand command, CancellationToken cancellationToken)
            {
                var phongban = await _phongbanRepository.S2_GetByIdAsync(command.Id);

                if (phongban == null)
                {
                    throw new ApiException($"PhongBan Not Found.");
                }
                else
                {
                    var result = await _phongbanRepository.DisableAsync(phongban);

                    if (result)
                    {
                        return new Response<int>(phongban.Id);
                    }
                    else
                    {
                        throw new ApiException($"PhongBan is Using, Cannot Delete.");
                    }
                    //phongban.Deleted = true;

                    //await _phongbanRepository.UpdateAsync(phongban);
                    //return new Response<int>(phongban.Id);
                }
            }
        }
    }
}
