using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.CreateViecBenNgoai
{
    public class CreateViecBenNgoaiCommand : IRequest<Response<string>>
    {
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

    public class CreateViecBenNgoaiCommandHandler : IRequestHandler<CreateViecBenNgoaiCommand, Response<string>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly IDiemDenRepositoryAsync _diemDenRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly string STATUS_APPROVED = "approved";

        public CreateViecBenNgoaiCommandHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync, IDiemDenRepositoryAsync diemDenRepositoryAsync, IMapper mapper)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _diemDenRepositoryAsync = diemDenRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateViecBenNgoaiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // kiem tra nhan vien gui don nghi phep co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} not found!");
                    return new Response<string>("VBN002");

                // kiem tra nhan vien thay the co ton tai ko?
                var nhanVienThayThe = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienThayTheId);
                if (nhanVienThayThe == null)
                    //return new Response<string>($"NhanVienThayTheId {request.NhanVienThayTheId} not found!");
                    return new Response<string>("VBN003");

                // kiem tra diem den id co ton tai khong
                var diemden = await _diemDenRepositoryAsync.S2_GetByIdAsync(request.DiemDenId);
                if(diemden == null)
                    //return new Response<string>($"DiemDenId {request.DiemDenId} not found!");
                    return new Response<string>("VBN004");

                // so sanh tgbd vs tgkt
                if(request.ThoiGianKetThuc <= request.ThoiGianBatDau)
                    //return new Response<string>($"ThoiGianKetThuc must be than ThoiGianBatDau.");
                    return new Response<string>("VBN005");

                var viecBenNgoai = _mapper.Map<ViecBenNgoai>(request);
                viecBenNgoai.TrangThaiXetDuyet = STATUS_APPROVED;
                viecBenNgoai.NguoiXetDuyetCap1Id = nhanvien.XetDuyetCap1;
                viecBenNgoai.NguoiXetDuyetCap2Id = nhanvien.XetDuyetCap2;

                await _viecBenNgoaiRepositoryAsync.AddAsync(viecBenNgoai);

                return new Response<string>(viecBenNgoai.Id.ToString(), null);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
