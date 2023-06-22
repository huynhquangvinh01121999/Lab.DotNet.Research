using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Commands.UpdateCaLamViec
{
    public class UpdateCaLamViecCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public DateTime BatDauNghi { get; set; }
        public DateTime KetThucNghi { get; set; }
        public bool? KhacNgay { get; set; }
        public string GhiChu { get; set; }
        public class UpdateCaLamViecCommandHandler : IRequestHandler<UpdateCaLamViecCommand, Response<int>>
        {
            private readonly ICaLamViecRepositoryAsync _calamviecRepository;
            public UpdateCaLamViecCommandHandler(ICaLamViecRepositoryAsync calamviecRepository)

            {
                _calamviecRepository = calamviecRepository;
            }
            public async Task<Response<int>> Handle(UpdateCaLamViecCommand command, CancellationToken cancellationToken)
            {
                var calamviec = await _calamviecRepository.S2_GetByIdAsync(command.Id);

                if (calamviec == null)
                {
                    throw new ApiException($"CaLamViec Not Found.");
                }
                else
                {
                    calamviec.BatDauNghi = command.BatDauNghi;
                    calamviec.GhiChu = command.GhiChu;
                    calamviec.GioBatDau = command.GioBatDau;
                    calamviec.GioKetThuc = command.GioKetThuc;
                    calamviec.KetThucNghi = command.KetThucNghi;
                    calamviec.KhacNgay = command.KhacNgay;
                    calamviec.TenJP = command.TenJP;
                    calamviec.TenVN = command.TenVN;
                    
                    await _calamviecRepository.UpdateAsync(calamviec);
                    return new Response<int>(calamviec.Id);
                }
            }
        }
    }
}
