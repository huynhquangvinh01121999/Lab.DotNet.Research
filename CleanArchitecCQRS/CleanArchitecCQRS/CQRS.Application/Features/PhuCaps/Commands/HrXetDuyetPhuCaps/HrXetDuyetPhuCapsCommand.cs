using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.HrXetDuyetPhuCaps
{
    public class HrXetDuyetPhuCapsCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string HR_TrangThai { get; set; }
        public IList<HrXetDuyetPhuCapModel> DanhSachXetDuyet { get; set; }
    }

    public class HrXetDuyetPhuCapsCommandHandler : IRequestHandler<HrXetDuyetPhuCapsCommand, Response<IList<string>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public HrXetDuyetPhuCapsCommandHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(HrXetDuyetPhuCapsCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                var pc = await _phuCapRepositoryAsync.S2_GetByGuidAsync(item.Id);

                if (pc is null)
                {
                    errorMessages.Add($"PhuCap ID: {item.Id} was not found.");
                    continue;
                }

                try
                {
                    pc.HRXetDuyetId = request.NhanVienId;
                    pc.HR_TrangThai = request.HR_TrangThai;
                    pc.HR_GhiChu = item.HR_GhiChu;
                    pc.XD_ThoiGianBatDau = item.XD_ThoiGianBatDau;
                    pc.XD_ThoiGianKetThuc = item.XD_ThoiGianKetThuc;
                    pc.XD_SoLanPhuCap = item.XD_SoLanPhuCap;
                    pc.XD_SoBuoiSang = item.XD_SoBuoiSang;
                    pc.XD_SoBuoiChieu = item.XD_SoBuoiChieu;
                    pc.XD_SoBuoiTrua = item.XD_SoBuoiTrua;
                    pc.XD_SoQuaDem = item.XD_SoQuaDem;

                    await _phuCapRepositoryAsync.UpdatePhuCapsAsync(pc);
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"PhuCap ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
