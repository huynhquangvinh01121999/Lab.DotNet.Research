using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetC1C2
{
    public class XetDuyetTimesheetC1C2Command : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string TrangThai { get; set; }
        public IList<XetDuyetTimesheetC1C2Model> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetTimesheetC1C2CommandHandler : IRequestHandler<XetDuyetTimesheetC1C2Command, Response<IList<string>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public XetDuyetTimesheetC1C2CommandHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetTimesheetC1C2Command request, CancellationToken cancellationToken)
        {
            bool flag = false;
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                flag = false;
                var tc = await _timesheetRepositoryAsync.S2_GetByGuidAsync(item.Id);

                if (tc is null)
                {
                    errorMessages.Add($"Timesheet ID: {item.Id} was not found.");
                    continue;
                }

                try
                {
                    if (tc.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                    {
                        tc.NXD1_TrangThai = request.TrangThai;
                        tc.NXD1_GhiChu = item.NXD1_GhiChu;
                        flag = true;
                    }

                    if (tc.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                    {
                        tc.NXD2_TrangThai = request.TrangThai;
                        tc.NXD2_GhiChu = item.NXD2_GhiChu;
                        flag = true;
                    }

                    if (flag)
                        await _timesheetRepositoryAsync.UpdateAsync(tc);

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
