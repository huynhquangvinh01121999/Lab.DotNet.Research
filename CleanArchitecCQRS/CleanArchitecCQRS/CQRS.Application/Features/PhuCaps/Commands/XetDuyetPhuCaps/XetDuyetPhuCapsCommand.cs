using EsuhaiHRM.Application.Enums;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCaps
{
    public class XetDuyetPhuCapsCommand : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string PhanLoai { get; set; }
        public string TrangThai { get; set; }
        public IList<PhuCapXetDuyetModel> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetPhuCapsCommandHandler : IRequestHandler<XetDuyetPhuCapsCommand, Response<IList<string>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public XetDuyetPhuCapsCommandHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetPhuCapsCommand request, CancellationToken cancellationToken)
        {
            if (!request.PhanLoai.ToUpper().Equals(LoaiXD.XD1) 
                && !request.PhanLoai.ToUpper().Equals(LoaiXD.XD2))
                return new Response<IList<string>>("Loại xét duyệt không hợp lệ.");

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
                    switch (request.PhanLoai.ToUpper())
                    {
                        case LoaiXD.XD1:
                            if (pc.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                            {
                                pc.NXD1_TrangThai = request.TrangThai;
                                pc.NXD1_GhiChu = item.GhiChu;
                                await _phuCapRepositoryAsync.UpdateAsync(pc);
                            }
                            break;
                        case LoaiXD.XD2:
                            if (pc.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                            {
                                pc.NXD2_TrangThai = request.TrangThai;
                                pc.NXD2_GhiChu = item.GhiChu;
                                await _phuCapRepositoryAsync.UpdateAsync(pc);
                            }
                            break;
                    }
                }
                catch(Exception ex) {
                    errorMessages.Add($"PhuCap ID: {item.Id} an exception error has occurred - {ex.Message}");
                }
            }

            if (errorMessages.Count > 0)
                return new Response<IList<string>>(false, errorMessages, null);

            return new Response<IList<string>>(null, "Xét duyệt thành công!");
        }
    }
}
