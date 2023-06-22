using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.UpdatePhuCaps
{
    public partial class UpdatePhuCapCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public int LoaiPhuCapId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }
    }

    public class UpdateCongTacCommandHandler : IRequestHandler<UpdatePhuCapCommand, Response<string>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;
        private readonly ILoaiPhuCapRepositoryAsync _loaiPhuCapRepositoryAsync;

        public UpdateCongTacCommandHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync, ILoaiPhuCapRepositoryAsync loaiPhuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
            _loaiPhuCapRepositoryAsync = loaiPhuCapRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdatePhuCapCommand request, CancellationToken cancellationToken)
        {
            var pc = await _phuCapRepositoryAsync.S2_GetByGuidAsync(request.Id);

            if (pc is null)
                return new Response<string>($"PhuCap ID: {request.Id} was not found.");

            var lpc = await _loaiPhuCapRepositoryAsync.GetByIdAsync(request.LoaiPhuCapId);
            if(lpc is null)
                return new Response<string>($"LoaiPhuCap ID: {request.LoaiPhuCapId} was not found.");

            try
            {
                pc.LoaiPhuCapId = request.LoaiPhuCapId;
                pc.ThoiGianBatDau = request.ThoiGianBatDau;
                pc.ThoiGianKetThuc = request.ThoiGianKetThuc;
                pc.MoTa = request.MoTa;

                await _phuCapRepositoryAsync.UpdatePhuCapsAsync(pc, lpc.Code);
                return new Response<string>(pc.Id.ToString(), null);
            }
            catch (Exception ex) {
                return new Response<string>($"An exception error has occurred: {ex.Message}");
            }
        }
    }
}
