using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.UpdateNhanVien
{
    public class UpdateNhanVienCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
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
        public string Avatar { get; set; }
        public string GhiChu { get; set; }
        public string AccountId { get; set; }
        public class UpdateNhanVienCommandHandler : IRequestHandler<UpdateNhanVienCommand, Response<Guid>>
        {
            private readonly INhanVienRepositoryAsync _nhanvienRepository;
            public UpdateNhanVienCommandHandler(INhanVienRepositoryAsync nhanvienRepository)
            {
                _nhanvienRepository = nhanvienRepository;
            }
            public async Task<Response<Guid>> Handle(UpdateNhanVienCommand command, CancellationToken cancellationToken)
            {
                var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(command.Id);

                if (nhanvien == null)
                {
                    throw new ApiException($"NhanVien Not Found.");
                }
                else
                {
                    nhanvien.MaNhanVien = command.MaNhanVien;
                    nhanvien.HoTenDemVN = command.HoTenDemVN;
                    nhanvien.TenVN = command.TenVN;
                    nhanvien.GioiTinhId = command.GioiTinhId;
                    nhanvien.NgaySinh = command.NgaySinh;
                    nhanvien.QuocTichId = command.QuocTichId;
                    nhanvien.HonNhanId = command.HonNhanId;
                    nhanvien.TonGiaoId = command.TonGiaoId;
                    nhanvien.DanTocId = command.DanTocId;
                    nhanvien.NguyenQuanId = command.NguyenQuanId;
                    nhanvien.DiaChiHienTai = command.DiaChiHienTai;
                    nhanvien.NoiSinhId = command.NoiSinhId;
                    nhanvien.HoKhau = command.HoKhau;
                    nhanvien.EmailCaNhan = command.EmailCaNhan;
                    nhanvien.DienThoaiCaNhan = command.DienThoaiCaNhan;
                    nhanvien.EmailCongTy = command.EmailCongTy;
                    nhanvien.DienThoaiCongTy = command.DienThoaiCongTy;
                    nhanvien.MaSoThue = command.MaSoThue;
                    nhanvien.SoTaiKhoan = command.SoTaiKhoan;
                    //nhanvien.CMND_So = command.CMND_So;
                    //nhanvien.CMND_NgayCap = command.CMND_NgayCap;
                    //nhanvien.CMND_NoiCapId = command.CMND_NoiCapId;
                    //nhanvien.Passport_So = command.Passport_So;
                    //nhanvien.Passport_NgayCap = command.Passport_NgayCap;
                    //nhanvien.Passport_NoiCapId = command.Passport_NoiCapId;
                    nhanvien.Username = command.Username;
                    nhanvien.CardNo = command.CardNo;
                    nhanvien.XetDuyetCap1 = command.XetDuyetCap1;
                    nhanvien.XetDuyetCap2 = command.XetDuyetCap2;
                    nhanvien.ChamCongOnline = command.ChamCongOnline;
                    nhanvien.MailNhacNho = command.MailNhacNho;
                    //Tránh bị thay đổi dữ liệu khi post null   START
                    //nhanvien.CongTyId = command.CongTyId;
                    //nhanvien.ChucVuId = command.ChucVuId;
                    //nhanvien.ChucDanhId = command.ChucDanhId;
                    //nhanvien.CapBac = command.CapBac;
                    //nhanvien.PhongId = command.PhongId;
                    //nhanvien.BanId = command.BanId;
                    //nhanvien.NhomId = command.NhomId;
                    //nhanvien.CaLamViecId = command.CaLamViecId;
                    //Tránh bị thay đổi dữ liệu khi post null   END
                    nhanvien.NgayBatDauLamViec = command.NgayBatDauLamViec;
                    nhanvien.TrangThaiId = command.TrangThaiId;
                    nhanvien.Avatar = command.Avatar;
                    nhanvien.AccountId = command.AccountId;

                    await _nhanvienRepository.UpdateAsync(nhanvien);
                    return new Response<Guid>(nhanvien.Id);
                }
            }
        }
    }
}
