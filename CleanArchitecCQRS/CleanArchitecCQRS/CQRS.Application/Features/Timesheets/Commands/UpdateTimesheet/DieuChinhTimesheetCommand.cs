using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.UpdateTimesheet
{
    public class DieuChinhTimesheetCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime? DieuChinh_GioVao { get; set; }
        public DateTime? DieuChinh_GioRa { get; set; }
        public string DieuChinh_GhiChu { get; set; }
        public class DieuChinhTimesheetCommandHandler : IRequestHandler<DieuChinhTimesheetCommand, Response<Guid>>
        {
            public readonly ITimesheetRepositoryAsync _timesheetRepository;
            public readonly INhanVienRepositoryAsync _nhanvienRepository;

            public DieuChinhTimesheetCommandHandler(ITimesheetRepositoryAsync timesheetRepository, INhanVienRepositoryAsync nhanvienRepository)
            {
                _timesheetRepository = timesheetRepository;
                _nhanvienRepository = nhanvienRepository;
            }

            public async Task<Response<Guid>> Handle(DieuChinhTimesheetCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var ts = await _timesheetRepository.S2_GetByGuidAsync(command.Id);
                    var nv = await _nhanvienRepository.S2_GetByIdAsync(command.NhanVienId);

                    if (ts == null)
                        return new Response<Guid>($"Timesheet Not Found.");

                    ts.DieuChinh_GhiChu = command.DieuChinh_GhiChu;
                    ts.DieuChinh_GioRa = new DateTime(ts.NgayLamViec.Year, ts.NgayLamViec.Month, ts.NgayLamViec.Day, command.DieuChinh_GioRa.Value.Hour, command.DieuChinh_GioRa.Value.Minute, command.DieuChinh_GioRa.Value.Second);
                    ts.DieuChinh_GioVao = new DateTime(ts.NgayLamViec.Year, ts.NgayLamViec.Month, ts.NgayLamViec.Day, command.DieuChinh_GioVao.Value.Hour, command.DieuChinh_GioVao.Value.Minute, command.DieuChinh_GioVao.Value.Second);
                    ts.TrangThai = "Submitted";
                    ts.NguoiXetDuyetCap1Id = nv.XetDuyetCap1;
                    ts.NguoiXetDuyetCap2Id = nv.XetDuyetCap2;
                    ts.NgayGoiDon = (ts.NgayGoiDon == null) ? DateTime.Now : ts.NgayGoiDon;

                    await _timesheetRepository.UpdateAsync(ts);
                    return new Response<Guid>(ts.Id);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message);
                }
            }
        }
    }
}
