using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CongTys.Commands.DeleteCongTyById
{
    public class DisableCongTyByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableCongTyByIdCommandHandler : IRequestHandler<DisableCongTyByIdCommand, Response<int>>
        {
            private readonly ICongTyRepositoryAsync _congtyRepository;
            public DisableCongTyByIdCommandHandler(ICongTyRepositoryAsync congtyRepository)
            {
                _congtyRepository = congtyRepository;
            }
            public async Task<Response<int>> Handle(DisableCongTyByIdCommand command, CancellationToken cancellationToken)
            {
                var congty = await _congtyRepository.S2_GetByIdAsync(command.Id);

                if (congty == null)
                {
                    throw new ApiException($"CongTy Not Found.");
                }
                else
                {
                    var result = await _congtyRepository.DisableAsync(congty);

                    if (result)
                    {
                        return new Response<int>(congty.Id);
                    }
                    else
                    {
                        throw new ApiException($"CongTy is Using, Cannot Delete.");
                    }
                    //congty.Deleted = true;

                    //await _congtyRepository.UpdateAsync(congty);
                    //return new Response<int>(congty.Id);
                }
            }
        }
    }
}
