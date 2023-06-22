using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.UpdateKhoaDaoTao
{
    public class UpdateKhoaDaoTaoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime? NgayDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiDaoTao { get; set; }
        public string MucDich { get; set; }
        public string DiaDiem { get; set; }
        public string GhiChu { get; set; }
        public class UpdateKhoaDaoTaoCommandHandler : IRequestHandler<UpdateKhoaDaoTaoCommand, Response<int>>
        {
            private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;
            public UpdateKhoaDaoTaoCommandHandler(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository)

            {
                _khoaDaoTaoRepository = khoaDaoTaoRepository;
            }
            public async Task<Response<int>> Handle(UpdateKhoaDaoTaoCommand command, CancellationToken cancellationToken)
            {
                var khoadaotao = await _khoaDaoTaoRepository.S2_GetByIdAsync(command.Id);

                if (khoadaotao == null)
                {
                    throw new ApiException($"KhoaDaoTao Not Found.");
                }
                else
                {
                    khoadaotao.TenVN = command.TenVN;
                    khoadaotao.TenJP = command.TenJP;
                    khoadaotao.NgayDaoTao = command.NgayDaoTao;
                    khoadaotao.NgayBatDau = command.NgayBatDau;
                    khoadaotao.NgayKetThuc = command.NgayKetThuc;
                    khoadaotao.NguoiDaoTao = command.NguoiDaoTao;
                    khoadaotao.MucDich = command.MucDich;
                    khoadaotao.DiaDiem = command.DiaDiem;
                    khoadaotao.GhiChu = command.GhiChu;
                    await _khoaDaoTaoRepository.UpdateAsync(khoadaotao);
                    return new Response<int>(khoadaotao.Id);
                }
            }
        }
    }
}
