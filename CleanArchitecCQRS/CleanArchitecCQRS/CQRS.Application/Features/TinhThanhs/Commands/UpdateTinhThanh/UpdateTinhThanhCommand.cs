using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Commands.UpdateTinhThanh
{
    public class UpdateTinhThanhCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string TenTinhVN { get; set; }
        public string TenTinhEN { get; set; }
        public string TenTinhJP { get; set; }
        public string MaTinh { get; set; }
        public class UpdateTinhThanhCommandHandler : IRequestHandler<UpdateTinhThanhCommand, Response<int>>
        {
            private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
            public UpdateTinhThanhCommandHandler(ITinhThanhRepositoryAsync tinhThanhRepository)

            {
                _tinhThanhRepository = tinhThanhRepository;
            }
            public async Task<Response<int>> Handle(UpdateTinhThanhCommand command, CancellationToken cancellationToken)
            {
                var tinhThanh = await _tinhThanhRepository.S2_GetByIdAsync(command.Id);

                if (tinhThanh == null)
                {
                    throw new ApiException($"TinhThanh Not Found.");
                }
                else
                {
                    tinhThanh.TenTinhVN = command.TenTinhVN;
                    tinhThanh.TenTinhEN = command.TenTinhEN;
                    tinhThanh.TenTinhJP = command.TenTinhJP;
                    tinhThanh.MaTinh = command.MaTinh;
                    await _tinhThanhRepository.UpdateAsync(tinhThanh);
                    return new Response<int>(tinhThanh.Id);
                }
            }
        }
    }
}
