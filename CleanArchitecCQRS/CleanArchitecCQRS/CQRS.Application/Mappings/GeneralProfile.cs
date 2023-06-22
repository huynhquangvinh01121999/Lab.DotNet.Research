using AutoMapper;
using EsuhaiHRM.Domain.Entities;
using System;
using EsuhaiHRM.Application.Features.CongTys.Commands.CreateCongTy;
using EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys;
using EsuhaiHRM.Application.Features.CaLamViecs.Commands.CreateCaLamViec;
using EsuhaiHRM.Application.Features.CaLamViecs.Queries.GetAllCaLamViecs;
using EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails;
using EsuhaiHRM.Application.Features.GroupMails.Commands.CreateGroupMail;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos;
using EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans;
using EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.CreateKhoaDaoTao;
using EsuhaiHRM.Application.Features.PhongBans.Commands.CreatePhongBan;
using EsuhaiHRM.Application.Features.TinhThanhs.Commands.CreateTinhThanh;
using EsuhaiHRM.Application.Features.NhanViens.Commands.CreateNhanVien;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;
using EsuhaiHRM.Application.Features.ChucVus.Queries.GetAllChucVus;
using EsuhaiHRM.Application.Features.ChucVus.Commands.CreateChucVu;
using EsuhaiHRM.Application.Features.DanhMucs.Queries.GetAllDanhMucs;

using EsuhaiHRM.Application.Features.ChiNhanhs.Commands.CreateChiNhanh;
using EsuhaiHRM.Application.Features.ChiNhanhs.Queries.GetAllChiNhanhs;
using EsuhaiHRM.Application.Features.TinNhans.Queries.GetAllTinNhans;
using EsuhaiHRM.Application.Features.TinNhans.Commands.CreateTinNhan;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForExport;
using EsuhaiHRM.Application.Features.Admin.Queries.GetRoles;
using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Features.Admin.Queries.GetUsers;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetAllTimesheets;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using EsuhaiHRM.Application.Features.TangCas.Commands.CreateTangCa;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCapsCountDay;
using EsuhaiHRM.Application.Features.LoaiPhuCaps.Queries.GetAllLoaiPhuCap;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCas;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanHr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanC1C2;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2C1C2;
using EsuhaiHRM.Application.Features.TangCas.Commands.UpdateTangCa;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3C1C2;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong;
using EsuhaiHRM.Application.Features.NghiLes.Commands.CreateNghiLe;
using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.CreateCauHinhNgayCong;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.CreateNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.CreateViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.UpdateViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.UpdateNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView;

namespace Esuhai.HRM.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateCongTyCommand, CongTy>();
            CreateMap<CongTy, GetAllCongTysViewModel>().ReverseMap();
            CreateMap<GetAllCongTysQuery, GetAllCongTysParameter>();

            CreateMap<CreateCaLamViecCommand, CaLamViec>();
            CreateMap<CaLamViec, GetAllCaLamViecsViewModel>().ReverseMap();
            CreateMap<GetAllCaLamViecsQuery, GetAllCaLamViecsParameter>();

            CreateMap<ChucVu, GetAllChucVusViewModel>().ReverseMap();
            CreateMap<CreateChucVuCommand, ChucVu>();
            CreateMap<GetAllChucVusQuery, GetAllChucVusParameter>();


            CreateMap<GroupMail, GetAllGroupMailsViewModel>().ReverseMap();
            CreateMap<CreateGroupMailCommand, GroupMail>();
            CreateMap<GetAllGroupMailsQuery, GetAllGroupMailsParameter>();

            CreateMap<KhoaDaoTao, GetAllKhoaDaoTaosViewModel>().ReverseMap();
            CreateMap<CreateKhoaDaoTaoCommand, KhoaDaoTao>();
            CreateMap<GetAllKhoaDaoTaosQuery, GetAllKhoaDaoTaosParameter>();

            CreateMap<PhongBan, GetAllPhongBansViewModel>().ReverseMap();
            CreateMap<PhongBan, GetAllPhongBansListDropDownViewModel>().ReverseMap();
            CreateMap<CreatePhongBanCommand, PhongBan>();
            CreateMap<GetAllPhongBansQuery, GetAllPhongBansParameter>();
            CreateMap<GetAllPhongBansListDropDownQuery, GetAllPhongBansListDropDownParameter>();

            CreateMap<TinhThanh, GetAllTinhThanhsViewModel>().ReverseMap();
            CreateMap<CreateTinhThanhCommand, TinhThanh>();
            CreateMap<GetAllTinhThanhsQuery, GetAllTinhThanhsParameter>();

            CreateMap<NhanVien, GetAllNhanViensViewModel>().ReverseMap();
            //CreateMap<NhanVien, NhanViensViewModel>().ReverseMap();
            CreateMap<NhanVien, NhanViensViewModel>().ForMember(nv => nv.NhanVienXetDuyetCap1HoTenVN
                                                               , vm => vm.MapFrom(vn => string.Format("{0} {1}"
                                                                                 , vn.NhanVienXetDuyetCap1.HoTenDemVN
                                                                                 , vn.NhanVienXetDuyetCap1.TenVN)))
                                                     .ForMember(nv => nv.NhanVienXetDuyetCap2HoTenVN
                                                               , vm => vm.MapFrom(vn => string.Format("{0} {1}"
                                                                                 , vn.NhanVienXetDuyetCap2.HoTenDemVN
                                                                                 , vn.NhanVienXetDuyetCap2.TenVN)));

            CreateMap<NhanVien, GetAllNhanViensForDropDownViewModel>().ForMember(nv => nv.HoTenVN
                                                                                , vm => vm.MapFrom(vn => String.Format("{0} {1}"
                                                                                                   , vn.HoTenDemVN, vn.TenVN)));
            CreateMap<NhanVien, GetAllNhanViensForPublicViewModel>().ReverseMap();
            CreateMap<GetAllNhanViensForPublicQuery, GetAllNhanViensParameter>();
            CreateMap<NhanVien, GetAllNhanViensForExportViewModel>().ReverseMap();
            CreateMap<GetAllNhanViensForExportQuery, GetAllNhanViensParameter>().ReverseMap();

            CreateMap<NhanVien, GetAllNhanViensForHomeViewModel>().ReverseMap();
            CreateMap<GetAllNhanViensForHomeQuery, RequestParameter>();


            CreateMap<CreateNhanVienCommand, NhanVien>();
            CreateMap<GetAllNhanViensQuery, GetAllNhanViensParameter>();
            CreateMap<GetAllNhanViensForDropDownQuery, GetAllNhanViensForDropDownParameter>();

            CreateMap<DanhMuc, GetAllDanhMucsViewModel>().ReverseMap();
            CreateMap<GetAllDanhMucsQuery, GetAllDanhMucsParameter>();

            CreateMap<ChiNhanh, GetAllChiNhanhsViewModel>().ReverseMap();
            CreateMap<CreateChiNhanhCommand, ChiNhanh>();
            CreateMap<GetAllChiNhanhsQuery, GetAllChiNhanhsParameter>();


            CreateMap<TinNhan, GetAllTinNhansViewModel>().ReverseMap();
            CreateMap<CreateTinNhanCommand, TinNhan>();
            CreateMap<GetAllTinNhansQuery, GetAllTinNhansParameter>();


            CreateMap<GetAllRolesQuery, RequestParameter>().ReverseMap();
            CreateMap<GetUsersByRoleQuery, RequestParameter>().ReverseMap();
            CreateMap<GetRolesByUserQuery, RequestParameter>().ReverseMap();

            CreateMap<Timesheet, GetAllTimesheetsViewModel>().ForMember(nv => nv.HoTenNhanVienVN, vm => vm.MapFrom(vn => String.Format("{0} {1}", vn.NhanVien.HoTenDemVN, vn.NhanVien.TenVN)));
            CreateMap<GetAllTimesheetsQuery, GetAllTimesheetsParameter>();

            CreateMap<Timesheet, GetTimesheetsByNhanVienViewModel>().ReverseMap();
            CreateMap<GetTimesheetsByNhanVienQuery, GetTimesheetsByNhanVienParameter>();
            CreateMap<GetTimesheetsHrViewParameter, GetTimesheetsHrViewQuery>();
            CreateMap<GetTimesheetsNotHrViewParameter, GetTimesheetsNotHrViewQuery>();

            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanHrQuery>();
            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanC1C2Query>();

            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanV2HrQuery>();
            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanV2C1C2Query>();

            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanV3HrQuery>();
            CreateMap<GetTimesheetsPhongBanParameter, GetTimesheetsPhongBanV3C1C2Query>();

            CreateMap<TongHopDuLieu, GetTongHopDuLieusByNhanVienViewModel>().ReverseMap();
            CreateMap<GetTongHopDuLieusByNhanVienQuery, GetTongHopDuLieusByNhanVienParameter>();
            CreateMap<QuanLyNgayCongParameter, QuanLyNgayCongQuery>();

            CreateMap<CreateTangCaCommand, TangCa>();

            CreateMap<GetTangCasQuery, GetTangCasParameter>();
            CreateMap<TangCa, GetTangCasViewModel>();
            CreateMap<TangCa, GetTangCasByNhanVienViewModel>();
            CreateMap<GetTangCasByNhanVienQuery, GetTangCasByNhanVienParameter>();
            CreateMap<GetTangCasHrViewParameter, GetTangCasHrViewQuery>();
            CreateMap<UpdateTangCaParameter, UpdateTangCaCommand>();

            CreateMap<CreatePhuCapCommand, PhuCap>();
            CreateMap<CreatePhuCapsCountDayCommand, PhuCap>();

            CreateMap<GetPhuCapsParameter, GetPhuCapsQuery>();
            CreateMap<GetPhuCapsHrViewParameter, GetPhuCapsHrViewQuery>();
            CreateMap<GetPhuCapsNotHrViewParameter, GetPhuCapsNotHrViewQuery>();

            CreateMap<PhuCap, GetPhuCapsViewModel>()
                .ForMember(vm => vm.HoTenNhanVienVN, vm => vm.MapFrom(pc => String.Format("{0} {1}", pc.NhanVien.HoTenDemVN, pc.NhanVien.TenVN)));

            //CreateMap<PhuCap, GetPhuCapsByNhanVienViewModel>()
            //    .ForMember(vm => vm.HoTenNhanVienVN, vm => vm.MapFrom(pc => String.Format("{0} {1}", pc.NhanVien.HoTenDemVN, pc.NhanVien.TenVN)));

            CreateMap<LoaiPhuCap, GetAllLoaiPhuCapViewModel>();

            CreateMap<CreateCauHinhNgayCongCommand, CauHinhNgayCong>();
            CreateMap<CreateNghiLeCommand, NghiLe>();

            /*
             * Mapping module Nghi phep
             */
            CreateMap<CreateNghiPhepParameters, CreateNghiPhepCommand>();
            CreateMap<CreateNghiPhepCommand, NghiPhep>();
            CreateMap<UpdateNghiPhepParameters, UpdateNghiPhepCommand>();
            CreateMap<GetNghiPhepsHrViewParameter, GetNghiPhepsHrViewQuery>();
            CreateMap<GetNghiPhepsNotHrViewParameter, GetNghiPhepsNotHrViewQuery>();

            /*
             * Mapping module Viec ben ngoai
             */
            CreateMap<CreateViecBenNgoaiParameters, CreateViecBenNgoaiCommand>();
            CreateMap<CreateViecBenNgoaiCommand, ViecBenNgoai>();
            CreateMap<UpdateViecBenNgoaiParameters, UpdateViecBenNgoaiCommand>();
            CreateMap<GetViecBenNgoaiHrViewParameter, GetViecBenNgoaiHrViewQuery>();
            CreateMap<GetViecBenNgoaisNotHrViewParameter, GetViecBenNgoaisNotHrViewQuery>();
        }
    }
}
