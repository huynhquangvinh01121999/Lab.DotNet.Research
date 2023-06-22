using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetHr
{
    public class XetDuyetTimesheetHrCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string HR_TrangThai { get; set; }
        public IList<XetDuyetTimesheetHrModel> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetTimesheetHrCommandHandler : IRequestHandler<XetDuyetTimesheetHrCommand, Response<IList<string>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public XetDuyetTimesheetHrCommandHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            this._timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetTimesheetHrCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                var ts = await _timesheetRepositoryAsync.S2_GetByGuidAsync(item.Id);

                if (ts is null)
                {
                    errorMessages.Add($"Timesheet ID: {item.Id} was not found.");
                    continue;
                }
                try
                {
                    ts.HRXetDuyetId = request.NhanVienId;
                    ts.HR_TrangThai = request.HR_TrangThai;
                    ts.HR_GioVao = item.HR_GioVao;
                    ts.HR_GioRa = item.HR_GioRa;
                    ts.HR_GhiChu = item.HR_GhiChu;

                    await _timesheetRepositoryAsync.UpdateAsync(ts);
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Timesheet ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
