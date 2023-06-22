using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCapsC1C2
{
    public class XetDuyetPhuCapsC1C2Command : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string TrangThai { get; set; }
        public IList<XetDuyetPhuCapsC1C2Model> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetPhuCapsC1C2CommandHandler : IRequestHandler<XetDuyetPhuCapsC1C2Command, Response<IList<string>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public XetDuyetPhuCapsC1C2CommandHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetPhuCapsC1C2Command request, CancellationToken cancellationToken)
        {
            bool flag = false;
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                flag = false;
                var pc = await _phuCapRepositoryAsync.S2_GetByGuidAsync(item.Id);

                if (pc is null)
                {
                    errorMessages.Add($"PhuCap ID: {item.Id} was not found.");
                    continue;
                }

                try
                {
                    // trong 1 role có thể là NXD1 or NXD2
                    // => nếu role thuộc NXD cấp 1 => update trạng thái
                    if (pc.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                    {
                        pc.NXD1_TrangThai = request.TrangThai;
                        pc.NXD1_GhiChu = item.NXD1_GhiChu;
                        flag = true;
                    }

                    // => nếu role thuộc NXD cấp 2 => update trạng thái
                    if (pc.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                    {
                        pc.NXD2_TrangThai = request.TrangThai;
                        pc.NXD2_GhiChu = item.NXD2_GhiChu;
                        flag = true;
                    }

                    if (flag)
                        await _phuCapRepositoryAsync.UpdateAsync(pc);

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
