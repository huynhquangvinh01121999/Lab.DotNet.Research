using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Commands.UpdateChiNhanh
{
    public class UpdateChiNhanhCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public int? LoaiChiNhanhId { get; set; }
        public Guid? Parent { get; set; }
        public string DiaChi { get; set; }
        public int? TinhId { get; set; }
        public bool? TrangThai { get; set; }
        public string GhiChu { get; set; }
        public class UpdateChiNhanhCommandHandler : IRequestHandler<UpdateChiNhanhCommand, Response<Guid>>
        {
            private readonly IChiNhanhRepositoryAsync _chiNhanhRepository;
            public UpdateChiNhanhCommandHandler(IChiNhanhRepositoryAsync chiNhanhRepository)
            {
                _chiNhanhRepository = chiNhanhRepository;
            }
            public async Task<Response<Guid>> Handle(UpdateChiNhanhCommand command, CancellationToken cancellationToken)
            {
                var chiNhanh = await _chiNhanhRepository.S2_GetByIdAsync(command.Id);

                if (chiNhanh == null)
                {
                    throw new ApiException($"ChiNhanh Not Found.");
                }
                else
                {
                    chiNhanh.TenVN = command.TenVN;
                    chiNhanh.TenJP = command.TenVN;
                    chiNhanh.LoaiChiNhanhId = command.LoaiChiNhanhId;
                    chiNhanh.Parent = command.Parent;
                    chiNhanh.DiaChi = command.DiaChi;
                    chiNhanh.TinhId = command.TinhId;
                    chiNhanh.TrangThai = command.TrangThai;
                    chiNhanh.GhiChu = command.GhiChu;

                    await _chiNhanhRepository.UpdateAsync(chiNhanh);
                    return new Response<Guid>(chiNhanh.Id);
                }
            }
        }
    }
}
