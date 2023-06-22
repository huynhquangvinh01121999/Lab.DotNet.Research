using EsuhaiHRM.Application.Enums;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCa
{
    public class XetDuyetTangCaCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string PhanLoai { get; set; }
        public string TrangThai { get; set; }
        public IList<TangCaXetDuyetModel> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetTangCaCommandHandler : IRequestHandler<XetDuyetTangCaCommand, Response<IList<string>>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public XetDuyetTangCaCommandHandler(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetTangCaCommand request, CancellationToken cancellationToken)
        {
            if (!request.PhanLoai.ToUpper().Equals(LoaiXD.XD1)
                && !request.PhanLoai.ToUpper().Equals(LoaiXD.XD2))
                return new Response<IList<string>>("Loại xét duyệt không hợp lệ.");

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
                    switch (request.PhanLoai.ToUpper())
                    {
                        case LoaiXD.XD1:
                            if (tc.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                            {
                                tc.NXD1_TrangThai = request.TrangThai;
                                tc.NXD1_GhiChu = item.GhiChu;
                                await _tangCaRepository.UpdateAsync(tc);
                            }
                            break;
                        case LoaiXD.XD2:
                            if (tc.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                            {
                                tc.NXD2_TrangThai = request.TrangThai;
                                tc.NXD2_GhiChu = item.GhiChu;
                                await _tangCaRepository.UpdateAsync(tc);
                            }
                            break;
                    }
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
