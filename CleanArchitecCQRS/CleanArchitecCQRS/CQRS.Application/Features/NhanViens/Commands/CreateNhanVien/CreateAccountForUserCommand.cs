using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Parameters;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.CreateNhanVien
{
    public partial class CreateAccountForUserCommand : IRequest<Response<string>>
    {
        public Guid NhanVienId { get; set; }
    }
    public class CreateAccountForUserCommandHandler : IRequestHandler<CreateAccountForUserCommand, Response<string>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;

        public CreateAccountForUserCommandHandler(INhanVienRepositoryAsync nhanvienRepository)
        {
            _nhanvienRepository = nhanvienRepository;
        }
        public async Task<Response<string>> Handle(CreateAccountForUserCommand request, CancellationToken cancellationToken)
        {
            var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(request.NhanVienId);

            if (nhanvien == null)
            {
                throw new ApiException($"NhanVien Not Found.");
            }
            else
            {
                nhanvien.AccountId = Guid.NewGuid().ToString();

                await _nhanvienRepository.UpdateAsync(nhanvien);

                var resultCreate = await _nhanvienRepository.CreateAccountForUser(request.NhanVienId);

                return new Response<string>(resultCreate, null);
            } 
        }
    }
}
