using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.HrXetDuyetTangCa
{
    public class HrXetDuyetTangCaCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string HR_TrangThai { get; set; }
        public IList<HrTangCaXetDuyetModel> DanhSachXetDuyet{ get; set; }
    }

    public class HrXetDuyetTangCaCommandHandler : IRequestHandler<HrXetDuyetTangCaCommand, Response<IList<string>>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public HrXetDuyetTangCaCommandHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }

        public async Task<Response<IList<string>>> Handle(HrXetDuyetTangCaCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                var tc = await _tangCaRepository.S2_GetByGuidAsync(item.Id);

                if (tc is null)
                {
                    errorMessages.Add($"TangCa ID: {item.Id} was not found.");
                    continue;
                }
                try
                {
                    tc.HRXetDuyetId = request.NhanVienId;
                    tc.HR_TrangThai = request.HR_TrangThai;
                    tc.SoGioCuoiTuan = item.SoGioCuoiTuan;
                    tc.SoGioDuocDuyet = item.SoGioDuocDuyet;
                    tc.SoGioNgayLe = item.SoGioNgayLe;
                    tc.SoGioNgayThuong = item.SoGioNgayThuong;
                    tc.HR_GhiChu = item.HR_GhiChu;

                    await _tangCaRepository.UpdateAsync(tc);
                }
                catch (Exception ex) {
                    errorMessages.Add($"TangCa ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
