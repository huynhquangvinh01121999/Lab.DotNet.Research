using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.UpdateNhanVien
{
    public class UpdateNhanVienAvatarCommand : IRequest<Response<UploadResponse>>
    {
        public Guid NhanVienId { get; set; }
        public string Avatar { get; set; }
    }
    public class UpdateNhanVienAvatarCommandHandler : IRequestHandler<UpdateNhanVienAvatarCommand, Response<UploadResponse>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        public UpdateNhanVienAvatarCommandHandler(INhanVienRepositoryAsync nhanvienRepository)
        {
            _nhanvienRepository = nhanvienRepository;
        }
        public async Task<Response<UploadResponse>> Handle(UpdateNhanVienAvatarCommand command, CancellationToken cancellationToken)
        {
            var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(command.NhanVienId);

            if (nhanvien == null)
            {
                throw new ApiException($"NhanVien Not Found.");
            }
            else
            {
                nhanvien.Avatar = command.Avatar;

                await _nhanvienRepository.UpdateAsync(nhanvien);
                return new Response<UploadResponse>(new UploadResponse(command.Avatar,null));
            }
        }
    }
}
