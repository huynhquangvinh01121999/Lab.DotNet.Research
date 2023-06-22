using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.CheckIn_Out
{
    public class CheckInCommand : IRequest<Response<string>>
    {
        public Guid NhanVienId { get; set; }
    }

    public class CheckInCommandHandler : IRequestHandler<CheckInCommand, Response<string>>
    {
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public CheckInCommandHandler(INhanVienRepositoryAsync nhanVienRepositoryAsync, ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<Response<string>> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var thoiGian = DateTime.Now;

                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);

                if (nhanvien == null)
                    return new Response<string>("TIS001");

                if (nhanvien.ChamCongOnline == false)
                    return new Response<string>("TIS002");

                if (nhanvien.CaLamViec == null)
                    return new Response<string>("TIS003");

                var ts = await _timesheetRepositoryAsync.S2_GetTimesheetByDate(request.NhanVienId, thoiGian);

                if (ts == null)
                {
                    var today = thoiGian.ToString("yyyy-MM-dd");
                    var timesheet = new Timesheet
                    {
                        Id = Guid.NewGuid(),
                        NhanVienId = request.NhanVienId,
                        NgayLamViec = DateTime.Parse(today),
                        Thang = DateTime.Now.Month,
                        Nam = DateTime.Now.Year,
                        CaLamViec_BatDau = DateTime.Parse($"{today} {nhanvien.CaLamViec.GioBatDau.ToString("HH:mm:ss")}"),
                        CaLamViec_KetThuc = DateTime.Parse($"{today} {nhanvien.CaLamViec.GioKetThuc.ToString("HH:mm:ss")}"),
                        CaLamViec_BatDauNghi = DateTime.Parse($"{today} {nhanvien.CaLamViec.BatDauNghi.ToString("HH:mm:ss")}"),
                        CaLamViec_KetThucNghi = DateTime.Parse($"{today} {nhanvien.CaLamViec.KetThucNghi.ToString("HH:mm:ss")}"),
                        GioVao = thoiGian,
                        NguoiXetDuyetCap1Id = nhanvien.XetDuyetCap1,
                        NguoiXetDuyetCap2Id = nhanvien.XetDuyetCap2
                    };
                    await _timesheetRepositoryAsync.AddAsync(timesheet);
                    return new Response<string>(timesheet.Id.ToString(), "TIS004");
                }
                else
                {
                    ts.GioVao = thoiGian;
                    await _timesheetRepositoryAsync.UpdateAsync(ts);
                    return new Response<string>(ts.Id.ToString(), "TIS004");
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
