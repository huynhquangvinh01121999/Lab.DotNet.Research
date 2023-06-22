using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.CreateNghiPhep
{
    public class CreateNghiPhepCommand : IRequest<Response<string>>
    {
        public Guid NhanVienId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float SoNgayDangKy { get; set; }
        public string MoTa { get; set; }
        public string CongViecThayThe { get; set; }
        public Guid NhanVienThayTheId { get; set; }
    }

    public class CreateNghiPhepCommandHandler : IRequestHandler<CreateNghiPhepCommand, Response<string>>
    {
        // bien thoi gian toi thieu so voi hien tai de duoc dang ky
        private int tgDangKyToiThieu = 8;
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateNghiPhepCommandHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync, IMapper mapper)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateNghiPhepCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // kiem tra nhan vien gui don nghi phep co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} not found!");
                    return new Response<string>("NPH002");

                // kiem tra nhan vien thay the co ton tai ko?
                var nhanVienThayThe = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienThayTheId);
                if(nhanVienThayThe == null)
                    //return new Response<string>($"NhanVienThayTheId {request.NhanVienThayTheId} not found!");
                    return new Response<string>("NPH003");

                // so sanh thoi gian ket thuc vs thoi gian bat dau
                if(request.ThoiGianKetThuc <= request.ThoiGianBatDau)
                    //return new Response<string>($"ThoiGianKetThuc must be than ThoiGianBatDau.");
                    return new Response<string>("NPH004");

                // tg dk don np phai lon hon thoi diem hien tai
                if((request.ThoiGianBatDau - DateTime.Now).TotalHours < Enums.NghiPhep.tgDangKyToiThieu)
                    //return new Response<string>($"ThoiGianBatDau must be than Time Now {Enums.NghiPhep.tgDangKyToiThieu} hours.");
                    return new Response<string>("NPH005");

                var nghiPhep = _mapper.Map<NghiPhep>(request);
                nghiPhep.NguoiXetDuyetCap1Id = nhanvien.XetDuyetCap1;
                nghiPhep.NguoiXetDuyetCap2Id = nhanvien.XetDuyetCap2;

                await _nghiPhepRepositoryAsync.AddAsync(nghiPhep);

                return new Response<string>(nghiPhep.Id.ToString(), null);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
