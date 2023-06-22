using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinNhans.Commands.UpdateTinNhan
{
    public class UpdateTinNhanCommand : IRequest<Response<TinNhan>>
    {
        public Guid Id { get; set; }
        public Guid? NhanVienGuiId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NhanHieu { get; set; }
        public string UrlThongTin { get; set; }
        public DateTime NgayGui { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public class UpdateTinNhanCommandHandler : IRequestHandler<UpdateTinNhanCommand, Response<TinNhan>>
        {
            private readonly ITinNhanRepositoryAsync _tinnhanRepository;
            public UpdateTinNhanCommandHandler(ITinNhanRepositoryAsync tinnhanRepository)
            {
                _tinnhanRepository = tinnhanRepository;
            }
            public async Task<Response<TinNhan>> Handle(UpdateTinNhanCommand command, CancellationToken cancellationToken)
            {
                var tinnhan = await _tinnhanRepository.S2_GetByIdAsync(command.Id);

                if (tinnhan == null)
                {
                    throw new ApiException($"TinNhan Not Found.");
                }
                else
                {
                    tinnhan.NhanVienGuiId = command.NhanVienGuiId;
                    tinnhan.TieuDe = command.TieuDe;
                    tinnhan.NoiDung = command.NoiDung;
                    tinnhan.NhanHieu = command.NhanHieu;
                    tinnhan.UrlThongTin = command.UrlThongTin;
                    tinnhan.NgayGui = command.NgayGui;
                    tinnhan.NgayTao = command.NgayTao;
                    tinnhan.GhiChu = command.GhiChu;

                    await _tinnhanRepository.UpdateAsync(tinnhan);
                    return new Response<TinNhan>(tinnhan);
                }
            }
        }
    }
}
