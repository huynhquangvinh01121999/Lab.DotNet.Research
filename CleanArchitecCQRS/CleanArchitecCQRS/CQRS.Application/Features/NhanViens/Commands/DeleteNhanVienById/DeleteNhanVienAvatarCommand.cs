using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.DeleteNhanVienById
{
    public class DeleteNhanVienAvatarCommand : IRequest<Response<int>>
    {
        public Guid NhanVienId { get; set; }
    }
    public class DeleteNhanVienAvatarCommandHandler : IRequestHandler<DeleteNhanVienAvatarCommand, Response<int>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        public DeleteNhanVienAvatarCommandHandler(INhanVienRepositoryAsync nhanvienRepository)
        {
            _nhanvienRepository = nhanvienRepository;
        }
        public async Task<Response<int>> Handle(DeleteNhanVienAvatarCommand command, CancellationToken cancellationToken)
        {
            var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(command.NhanVienId);

            if (nhanvien == null)
            {
                throw new ApiException($"NhanVien Not Found.");
            }
            else
            {
                nhanvien.Avatar = null;

                await _nhanvienRepository.UpdateAsync(nhanvien);
                return new Response<int>(1);
            }
        }
    }
}
