using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChucVus.Commands.UpdateChucVu
{
    public class UpdateChucVuCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public int? CapBac { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public class UpdateChucVuCommandHandler : IRequestHandler<UpdateChucVuCommand, Response<int>>
        {
            private readonly IChucVuRepositoryAsync _chucvuRepository;
            public UpdateChucVuCommandHandler(IChucVuRepositoryAsync chucvuRepository)

            {
                _chucvuRepository = chucvuRepository;
            }
            public async Task<Response<int>> Handle(UpdateChucVuCommand command, CancellationToken cancellationToken)
            {
                var chucvu = await _chucvuRepository.S2_GetByIdAsync(command.Id);

                if (chucvu == null)
                {
                    throw new ApiException($"ChucVu Not Found.");
                }
                else
                {
                    chucvu.GhiChu = command.GhiChu;
                    chucvu.PhanLoai = command.PhanLoai;
                    chucvu.TenEN = command.TenEN;
                    chucvu.TenJP = command.TenJP;
                    chucvu.TenVN = command.TenVN;
                    chucvu.CapBac = command.CapBac;
                                        
                    await _chucvuRepository.UpdateAsync(chucvu);
                    return new Response<int>(chucvu.Id);
                }
            }
        }
    }
}
