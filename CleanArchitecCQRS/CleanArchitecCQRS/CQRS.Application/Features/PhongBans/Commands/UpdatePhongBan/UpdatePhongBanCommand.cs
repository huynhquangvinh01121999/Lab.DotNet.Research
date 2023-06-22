using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhongBans.Commands.UpdatePhongBan
{
    public class UpdatePhongBanCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public Guid? TruongBoPhanId { get; set; }
        public int? Parent { get; set; }
        public int? GroupMailId { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public class UpdatePhongBanCommandHandler : IRequestHandler<UpdatePhongBanCommand, Response<int>>
        {
            private readonly IPhongBanRepositoryAsync _phongBanRepository;
            public UpdatePhongBanCommandHandler(IPhongBanRepositoryAsync phongBanRepository)

            {
                _phongBanRepository = phongBanRepository;
            }
            public async Task<Response<int>> Handle(UpdatePhongBanCommand command, CancellationToken cancellationToken)
            {
                var phongBan = await _phongBanRepository.S2_GetByIdAsync(command.Id);

                if (phongBan == null)
                {
                    throw new ApiException($"PhongBan Not Found.");
                }
                else
                {
                    phongBan.TenVN = command.TenVN;
                    phongBan.TenEN = command.TenEN;
                    phongBan.TenJP = command.TenJP;
                    phongBan.PhanLoai = command.PhanLoai;
                    phongBan.TruongBoPhanId = command.TruongBoPhanId;
                    phongBan.Parent = command.Parent;
                    phongBan.GroupMailId = command.GroupMailId;
                    phongBan.TrangThai = command.TrangThai;
                    phongBan.GhiChu = command.GhiChu;
                    await _phongBanRepository.UpdateAsync(phongBan);
                    return new Response<int>(phongBan.Id);
                }
            }
        }
    }
}
