using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.UpdateTangCa
{
    public class UpdateTangCaCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DateTime NgayTangCa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float? SoGioDangKy { get; set; }
        public string MoTa { get; set; }

        public class UpdateTangCaCommandHandler : IRequestHandler<UpdateTangCaCommand, Response<string>>
        {
            private readonly ITangCaRepositoryAsync _tangCaRepository;
            public UpdateTangCaCommandHandler(ITangCaRepositoryAsync tangCaRepository)
            {
                _tangCaRepository = tangCaRepository;
            }
            public async Task<Response<string>> Handle(UpdateTangCaCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var ot = await _tangCaRepository.S2_GetByGuidAsync(command.Id);

                    if (ot == null)
                        return new Response<string>($"TangCa Id {command.Id} was not found.");

                    ot.NgayTangCa = command.NgayTangCa;
                    ot.ThoiGianBatDau = command.ThoiGianBatDau;
                    ot.ThoiGianKetThuc = command.ThoiGianKetThuc;
                    ot.SoGioDangKy = string.IsNullOrEmpty(command.SoGioDangKy.ToString()) ? ot.SoGioDangKy : command.SoGioDangKy;
                    ot.MoTa = string.IsNullOrEmpty(command.MoTa) ? ot.MoTa : command.MoTa;

                    await _tangCaRepository.UpdateAsync(ot);
                    return new Response<string>(ot.Id.ToString(), null);
                }
                catch (Exception ex) {
                    throw new ApiException(ex.Message);
                }
            }
        }
    }
}
