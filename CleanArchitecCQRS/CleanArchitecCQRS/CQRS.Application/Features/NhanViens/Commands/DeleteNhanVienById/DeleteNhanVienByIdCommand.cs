using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.DeleteNhanVienById
{
    public class DeleteNhanVienByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public class DeleteNhanVienByIdCommandHandler : IRequestHandler<DeleteNhanVienByIdCommand, Response<Guid>>
        {
            private readonly INhanVienRepositoryAsync _nhanvienRepository;
            public DeleteNhanVienByIdCommandHandler(INhanVienRepositoryAsync nhanvienRepository)
            {
                _nhanvienRepository = nhanvienRepository;
            }
            public async Task<Response<Guid>> Handle(DeleteNhanVienByIdCommand command, CancellationToken cancellationToken)
            {
                var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(command.Id);
                if (nhanvien == null) throw new ApiException($"NhanVien Not Found.");
                await _nhanvienRepository.DeleteAsync(nhanvien);
                return new Response<Guid>(nhanvien.Id);
            }
        }
    }
}
