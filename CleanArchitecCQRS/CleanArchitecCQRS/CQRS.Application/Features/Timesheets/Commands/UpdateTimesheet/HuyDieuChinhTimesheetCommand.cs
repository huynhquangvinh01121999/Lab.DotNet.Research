using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.UpdateTimesheet
{
    public class HuyDieuChinhTimesheetCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public class HuyDieuChinhTimesheetCommandHandler : IRequestHandler<HuyDieuChinhTimesheetCommand, Response<Guid>>
        {
            public readonly ITimesheetRepositoryAsync _timesheetRepository;
            public HuyDieuChinhTimesheetCommandHandler(ITimesheetRepositoryAsync timesheetRepository) {
                _timesheetRepository = timesheetRepository;
            }
            public async Task<Response<Guid>> Handle(HuyDieuChinhTimesheetCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var ts = await _timesheetRepository.S2_GetByGuidAsync(command.Id);

                    if (ts == null)
                        return new Response<Guid>($"Timesheet Not Found.");

                    ts.DieuChinh_GhiChu = null;
                    ts.DieuChinh_GioRa = null;
                    ts.DieuChinh_GioVao = null;
                    ts.NguoiXetDuyetCap1Id = null;
                    ts.NguoiXetDuyetCap2Id = null;
                    ts.NgayGoiDon = null;
                    ts.TrangThai = null;

                    ts.NXD1_GhiChu = null;
                    ts.NXD1_TrangThai = null;

                    ts.NXD2_GhiChu = null;
                    ts.NXD2_TrangThai = null;

                    ts.HRXetDuyetId = null;
                    ts.HR_GhiChu = null;
                    ts.HR_TrangThai = null;
                    ts.HR_GioRa = null;
                    ts.HR_GioVao = null;

                    await _timesheetRepository.UpdateAsync(ts);
                    return new Response<Guid>(ts.Id);
                }
                catch (Exception ex) {
                    throw new ApiException(ex.Message);
                }
            }
        }
    }
}
