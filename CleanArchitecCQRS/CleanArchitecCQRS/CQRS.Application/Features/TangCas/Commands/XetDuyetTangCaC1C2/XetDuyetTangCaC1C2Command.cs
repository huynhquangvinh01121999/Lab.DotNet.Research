using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCaC1C2
{
    public class XetDuyetTangCaC1C2Command : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string TrangThai { get; set; }
        public IList<XetDuyetTangCaC1C2Model> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetTangCaC1C2CommandHandler : IRequestHandler<XetDuyetTangCaC1C2Command, Response<IList<string>>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public XetDuyetTangCaC1C2CommandHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetTangCaC1C2Command request, CancellationToken cancellationToken)
        {
            bool flag = false;
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                flag = false;
                var tc = await _tangCaRepository.S2_GetByGuidAsync(item.Id);

                if (tc is null)
                {
                    errorMessages.Add($"TangCa ID: {item.Id} was not found.");
                    continue;
                }

                try
                {
                    // trong 1 role có thể là NXD1 or NXD2
                    // => nếu role thuộc NXD cấp 1 => update trạng thái
                    if (tc.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                    {
                        tc.NXD1_TrangThai = request.TrangThai;
                        tc.NXD1_GhiChu = item.NXD1_GhiChu;
                        flag = true;
                    }

                    // => nếu role thuộc NXD cấp 2 => update trạng thái
                    if (tc.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                    {
                        tc.NXD2_TrangThai = request.TrangThai;
                        tc.NXD2_GhiChu = item.NXD2_GhiChu;
                        flag = true;
                    }

                    if (flag)
                        await _tangCaRepository.UpdateAsync(tc);

                }
                catch (Exception ex)
                {
                    errorMessages.Add($"TangCa ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
