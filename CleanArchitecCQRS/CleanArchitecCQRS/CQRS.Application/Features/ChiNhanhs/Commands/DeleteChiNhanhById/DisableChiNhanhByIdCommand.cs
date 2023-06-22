using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Commands.DeleteChiNhanhById
{
    public class DisableChiNhanhByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public class DisableChiNhanhByIdCommandHandler : IRequestHandler<DisableChiNhanhByIdCommand, Response<Guid>>
        {
            private readonly IChiNhanhRepositoryAsync _chinhanhRepository;
            public DisableChiNhanhByIdCommandHandler(IChiNhanhRepositoryAsync chinhanhRepository)
            {
                _chinhanhRepository = chinhanhRepository;
            }
            public async Task<Response<Guid>> Handle(DisableChiNhanhByIdCommand command, CancellationToken cancellationToken)
            {
                var chinhanh = await _chinhanhRepository.S2_GetByIdAsync(command.Id);

                if (chinhanh == null)
                {
                    throw new ApiException($"ChiNhanh Not Found.");
                }
                else
                {
                    var result = await _chinhanhRepository.DisableAsync(chinhanh);

                    if (result)
                    {
                        return new Response<Guid>(chinhanh.Id);
                    }
                    else
                    {
                        throw new ApiException($"ChiNhanh is Using, Cannot Delete.");
                    }
                    //chinhanh.Deleted = true;

                    //await _chinhanhRepository.UpdateAsync(chinhanh);
                    //return new Response<Guid>(chinhanh.Id);
                }
            }
        }
    }
}
