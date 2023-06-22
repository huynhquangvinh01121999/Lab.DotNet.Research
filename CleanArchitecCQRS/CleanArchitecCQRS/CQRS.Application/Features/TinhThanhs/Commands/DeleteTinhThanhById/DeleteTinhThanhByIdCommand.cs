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
    public class DeleteTinhThanhByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteTinhThanhByIdCommandHandler : IRequestHandler<DeleteTinhThanhByIdCommand, Response<int>>
        {
            private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
            public DeleteTinhThanhByIdCommandHandler(ITinhThanhRepositoryAsync tinhThanhRepository)
            {
                _tinhThanhRepository = tinhThanhRepository;
            }
            public async Task<Response<int>> Handle(DeleteTinhThanhByIdCommand command, CancellationToken cancellationToken)
            {
                var tinhThanh = await _tinhThanhRepository.S2_GetByIdAsync(command.Id);
                if (tinhThanh == null) throw new ApiException($"TinhThanh Not Found.");
                await _tinhThanhRepository.DeleteAsync(tinhThanh);
                return new Response<int>(tinhThanh.Id);
            }
        }
    }
}
