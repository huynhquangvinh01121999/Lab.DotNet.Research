using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepC1C2
{
    public class XetDuyetNghiPhepC1C2Command : IRequest<Response<IList<string>>>
    {
        public Guid NhanVienId { get; set; }
        public string TrangThai { get; set; }
        public IList<XetDuyetNghiPhepC1C2Model> DanhSachXetDuyet { get; set; }
    }

    public class XetDuyetNghiPhepC1C2CommandHandler : IRequestHandler<XetDuyetNghiPhepC1C2Command, Response<IList<string>>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;

        public XetDuyetNghiPhepC1C2CommandHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
        }

        public async Task<Response<IList<string>>> Handle(XetDuyetNghiPhepC1C2Command request, CancellationToken cancellationToken)
        {
            bool flag = false;
            List<string> errorMessages = new List<string>();
            foreach (var item in request.DanhSachXetDuyet)
            {
                try
                {
                    flag = false;
                    var nghiphep = await _nghiPhepRepositoryAsync.S2_GetByGuidAsync(item.Id);

                    if (nghiphep == null)
                    {
                        errorMessages.Add($"NghiPhep ID: {item.Id} not found.");
                        continue;
                    }


                    if (nghiphep.NguoiXetDuyetCap1Id.Equals(request.NhanVienId))
                    {
                        nghiphep.NXD1_TrangThai = request.TrangThai;
                        nghiphep.NXD1_GhiChu = item.NXD1_GhiChu;
                        flag = true;
                    }

                    if (nghiphep.NguoiXetDuyetCap2Id.Equals(request.NhanVienId))
                    {
                        nghiphep.NXD2_TrangThai = request.TrangThai;
                        nghiphep.NXD2_GhiChu = item.NXD2_GhiChu;
                        flag = true;
                    }

                    if (flag)
                        await _nghiPhepRepositoryAsync.UpdateAsync(nghiphep);

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
