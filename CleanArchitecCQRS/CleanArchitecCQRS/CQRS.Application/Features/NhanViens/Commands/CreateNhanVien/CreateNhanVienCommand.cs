using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.CreateNhanVien
{
    public partial class CreateNhanVienCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MaNhanVien { get; set; }
        public string HoTenDemVN { get; set; }
        public string TenVN { get; set; }
        public int? GioiTinhId { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? QuocTichId { get; set; }
        public int? HonNhanId { get; set; }
        public int? TonGiaoId { get; set; }
        public int? DanTocId { get; set; }
        public int? NguyenQuanId { get; set; }
        public string DiaChiHienTai { get; set; }
        public int? NoiSinhId { get; set; }
        public string HoKhau { get; set; }
        public string EmailCaNhan { get; set; }
        public string DienThoaiCaNhan { get; set; }
        public string EmailCongTy { get; set; }
        public string DienThoaiCongTy { get; set; }
        public string MaSoThue { get; set; }
        public string SoTaiKhoan { get; set; }
        //public string CMND_So { get; set; }
        //public DateTime? CMND_NgayCap { get; set; }
        //public int? CMND_NoiCapId { get; set; }
        //public string Passport_So { get; set; }
        //public DateTime? Passport_NgayCap { get; set; }
        //public int? Passport_NoiCapId { get; set; }
        public string Username { get; set; }
        public string CardNo { get; set; }
        public Guid? XetDuyetCap1 { get; set; }
        public Guid? XetDuyetCap2 { get; set; }
        public bool? ChamCongOnline { get; set; }
        public bool? MailNhacNho { get; set; }
        public int? CongTyId { get; set; }
        public int? ChucVuId { get; set; }
        public int? ChucDanhId { get; set; }
        public string CapBac { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
        public int? NhomId { get; set; }
        public int? CaLamViecId { get; set; }
        public DateTime? NgayBatDauLamViec { get; set; }
        public int? TrangThaiId { get; set; }
        public double? ThamNien { get; set; } = 0;
        public string Avatar { get; set; }
        public string GhiChu { get; set; }
        public string AccountId { get; set; }
    }
    public class CreateNhanVienCommandHandler : IRequestHandler<CreateNhanVienCommand, Response<Guid>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public CreateNhanVienCommandHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateNhanVienCommand request, CancellationToken cancellationToken)
        {
            request.MaNhanVien = _nhanvienRepository.GenerateMaNV((int)request.CongTyId);
            
            var nhanvien = _mapper.Map<NhanVien>(request);

            await _nhanvienRepository.AddAsync(nhanvien);

            return new Response<Guid>(nhanvien.Id);
        }
    }

}
