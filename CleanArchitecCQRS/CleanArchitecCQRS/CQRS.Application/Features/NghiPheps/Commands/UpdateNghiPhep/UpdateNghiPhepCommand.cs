using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.UpdateNghiPhep
{
    public class UpdateNghiPhepCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float SoNgayDangKy { get; set; }
        public string MoTa { get; set; }
        public string CongViecThayThe { get; set; }
        public Guid NhanVienThayTheId { get; set; }
    }

    public class UpdateNghiPhepCommandHandler : IRequestHandler<UpdateNghiPhepCommand, Response<string>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;

        public UpdateNghiPhepCommandHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateNghiPhepCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // lay ra thong tin don nghi phep
                var nghiPhep = await _nghiPhepRepositoryAsync.S2_GetByGuidAsync(request.Id);
                if(nghiPhep == null)
                    //return new Response<string>($"NghiPhep Id {request.Id} not found!");
                    return new Response<string>("NPH001");

                // kiem tra nhan vien gui don nghi phep co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} not found!");
                    return new Response<string>("NPH002");

                // kiem tra nhanVienId cua don nghi phep co khop voi nhanVienId dang login hay khong
                if (nghiPhep.NhanVienId != request.NhanVienId)
                    //return new Response<string>($"Do not permission!");
                    return new Response<string>("NPH008");

                // kiem tra nhan vien thay the co ton tai ko?
                var nhanVienThayThe = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienThayTheId);
                if (nhanVienThayThe == null)
                    //return new Response<string>($"NhanVienThayTheId {request.NhanVienThayTheId} not found!");
                    return new Response<string>("NPH003");

                // so sanh thoi gian ket thuc vs thoi gian bat dau
                if (request.ThoiGianKetThuc <= request.ThoiGianBatDau)
                    //return new Response<string>($"ThoiGianKetThuc must be than ThoiGianBatDau.");
                    return new Response<string>("NPH004");

                // tg dk don np phai lon hon thoi diem hien tai
                if ((request.ThoiGianBatDau - DateTime.Now).TotalHours < Enums.NghiPhep.tgDangKyToiThieu)
                    //return new Response<string>($"ThoiGianBatDau must be than Time Now {Enums.NghiPhep.tgDangKyToiThieu} hours.");
                    return new Response<string>("NPH005");

                nghiPhep.ThoiGianBatDau = request.ThoiGianBatDau;
                nghiPhep.ThoiGianKetThuc = request.ThoiGianKetThuc;
                nghiPhep.SoNgayDangKy = request.SoNgayDangKy;
                nghiPhep.MoTa = request.MoTa;
                nghiPhep.CongViecThayThe = request.CongViecThayThe;
                nghiPhep.NhanVienThayTheId = request.NhanVienThayTheId;

                await _nghiPhepRepositoryAsync.UpdateAsync(nghiPhep);

                return new Response<string>(nghiPhep.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
