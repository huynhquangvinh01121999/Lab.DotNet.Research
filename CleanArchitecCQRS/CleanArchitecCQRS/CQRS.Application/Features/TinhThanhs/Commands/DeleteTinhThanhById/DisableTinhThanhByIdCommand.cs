using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Commands.DeleteTinhThanhById
{
    public class DisableTinhThanhByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableTinhThanhByIdCommandHandler : IRequestHandler<DisableTinhThanhByIdCommand, Response<int>>
        {
            private readonly ITinhThanhRepositoryAsync _tinhthanhRepository;
            public DisableTinhThanhByIdCommandHandler(ITinhThanhRepositoryAsync tinhthanhRepository)
            {
                _tinhthanhRepository = tinhthanhRepository;
            }
            public async Task<Response<int>> Handle(DisableTinhThanhByIdCommand command, CancellationToken cancellationToken)
            {
                var tinhthanh = await _tinhthanhRepository.S2_GetByIdAsync(command.Id);

                if (tinhthanh == null)
                {
                    throw new ApiException($"TinhThanh Not Found.");
                }
                else
                {
                    var result = await _tinhthanhRepository.DisableAsync(tinhthanh);

                    if (result)
                    {
                        return new Response<int>(tinhthanh.Id);
                    }
                    else
                    {
                        throw new ApiException($"TinhThanh is Using, Cannot Delete.");
                    }
                    //tinhthanh.Deleted = true;

                    //await _tinhthanhRepository.UpdateAsync(tinhthanh);
                    //return new Response<int>(tinhthanh.Id);
                }
            }
        }
    }
}
