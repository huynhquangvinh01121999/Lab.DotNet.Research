using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepHr
{
    public class XetDuyetNghiPhepHrCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string HR_TrangThai { get; set; }
        public IList<XetDuyetNghiPhepHrModel> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetNghiPhepHrCommandHandler : IRequestHandler<XetDuyetNghiPhepHrCommand, Response<IList<string>>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;

        public XetDuyetNghiPhepHrCommandHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetNghiPhepHrCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                try
                {
                    var ts = await _nghiPhepRepositoryAsync.S2_GetByGuidAsync(item.Id);

                    if (ts is null)
                    {
                        errorMessages.Add($"NghiPhep ID: {item.Id} not found.");
                        continue;
                    }

                    ts.HRXetDuyetId = request.NhanVienId;
                    ts.HR_TrangThai = request.HR_TrangThai;
                    ts.TrangThaiDangKy = item.TrangThaiDangKy;
                    ts.TrangThaiNghi = item.TrangThaiNghi;
                    ts.HR_GhiChu = item.HR_GhiChu;

                    await _nghiPhepRepositoryAsync.UpdateAsync(ts);
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"NghiPhep ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
