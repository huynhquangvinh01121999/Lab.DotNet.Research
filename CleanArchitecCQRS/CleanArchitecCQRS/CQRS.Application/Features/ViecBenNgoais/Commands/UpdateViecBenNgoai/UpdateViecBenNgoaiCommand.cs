using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.UpdateViecBenNgoai
{
    public class UpdateViecBenNgoaiCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string LoaiCongTac { get; set; }
        public string MoTa { get; set; }
        public string DiaDiem { get; set; }
        public string NguoiGap { get; set; }
        public float SoGio { get; set; }
        public int DiemDenId { get; set; }
        public Guid NhanVienThayTheId { get; set; }
    }

    public class UpdateViecBenNgoaiCommandHandler : IRequestHandler<UpdateViecBenNgoaiCommand, Response<string>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly IDiemDenRepositoryAsync _diemDenRepositoryAsync;

        public UpdateViecBenNgoaiCommandHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync, IDiemDenRepositoryAsync diemDenRepositoryAsync)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _diemDenRepositoryAsync = diemDenRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateViecBenNgoaiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // kiem tra vbn co ton tai ko?
                var viecBenNgoai = await _viecBenNgoaiRepositoryAsync.S2_GetByGuidAsync(request.Id);
                if(viecBenNgoai == null)
                    //return new Response<string>($"ViecBenNgoai {request.Id} not found!");
                    return new Response<string>("VBN001");

                // kiem tra user đang login co trung vs user so huu don vbn can dieu chinh ko?
                if (viecBenNgoai.NhanVienId != request.NhanVienId)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} invalid!");
                    return new Response<string>("VBN008");

                // kiem tra user gui don dieu chinh vbn co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} not found!");
                    return new Response<string>("VBN002");

                // kiem tra nhan vien thay the co ton tai ko?
                var nhanVienThayThe = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienThayTheId);
                if (nhanVienThayThe == null)
                    //return new Response<string>($"NhanVienThayTheId {request.NhanVienThayTheId} not found!");
                    return new Response<string>($"VBN003");

                // kiem tra diem den id co ton tai khong
                var diemden = await _diemDenRepositoryAsync.S2_GetByIdAsync(request.DiemDenId);
                if (diemden == null)
                    //return new Response<string>($"DiemDenId {request.DiemDenId} not found!");
                    return new Response<string>("VBN004");

                // so sanh tgbd vs tgkt
                if (request.ThoiGianKetThuc <= request.ThoiGianBatDau)
                    //return new Response<string>($"ThoiGianKetThuc must be than ThoiGianBatDau.");
                    return new Response<string>("VBN005");

                // update
                viecBenNgoai.ThoiGianBatDau = request.ThoiGianBatDau;
                viecBenNgoai.ThoiGianKetThuc = request.ThoiGianKetThuc;
                viecBenNgoai.MoTa = request.MoTa;
                viecBenNgoai.LoaiCongTac = request.LoaiCongTac;
                viecBenNgoai.DiaDiem = request.DiaDiem;
                viecBenNgoai.NguoiGap = viecBenNgoai.NguoiGap;
                viecBenNgoai.SoGio = viecBenNgoai.SoGio;
                viecBenNgoai.DiemDenId = request.DiemDenId;
                viecBenNgoai.NhanVienThayTheId = request.NhanVienThayTheId;

                await _viecBenNgoaiRepositoryAsync.UpdateAsync(viecBenNgoai);

                return new Response<string>(viecBenNgoai.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
