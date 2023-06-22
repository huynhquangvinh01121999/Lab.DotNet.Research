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
    public class DeleteChiNhanhByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public class DeleteChiNhanhByIdCommandHandler : IRequestHandler<DeleteChiNhanhByIdCommand, Response<Guid>>
        {
            private readonly IChiNhanhRepositoryAsync _chiNhanhRepository;
            public DeleteChiNhanhByIdCommandHandler(IChiNhanhRepositoryAsync chiNhanhRepository)
            {
                _chiNhanhRepository = chiNhanhRepository;
            }
            public async Task<Response<Guid>> Handle(DeleteChiNhanhByIdCommand command, CancellationToken cancellationToken)
            {
                var chiNhanh = await _chiNhanhRepository.S2_GetByIdAsync(command.Id);
                if (chiNhanh == null) throw new ApiException($"ChiNhanh Not Found.");
                await _chiNhanhRepository.DeleteAsync(chiNhanh);
                return new Response<Guid>(chiNhanh.Id);
            }
        }
    }
}
