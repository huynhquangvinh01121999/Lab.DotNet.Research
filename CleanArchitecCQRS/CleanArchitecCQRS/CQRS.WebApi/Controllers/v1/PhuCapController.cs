using AutoMapper;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCapsCountDay;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.DeletePhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.HrXetDuyetPhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.UpdatePhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCapsC1C2;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCaps;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PhuCapController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly IMapper _mapper;

        public PhuCapController(IAccountService accountService, INhanVienRepositoryAsync nhanVienRepositoryAsync, 
                                IMapper mapper)
        {
            _accountService = accountService;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetPhuCapsParameter filter)
        {
            var query = _mapper.Map<GetPhuCapsQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetAllViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetPhuCapsHrViewParameter filter)
        {
            var query = _mapper.Map<GetPhuCapsHrViewQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetAllNotViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetPhuCapsNotHrViewParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetPhuCapsNotHrViewQuery()
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    ThoiGianBatDau = filter.ThoiGianBatDau,
                    ThoiGianKetThuc = filter.ThoiGianKetThuc,
                    TrangThai = filter.TrangThai,
                    Keyword = filter.Keyword
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetByNhanVien")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetPhuCapsByNhanVienParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetPhuCapsByNhanVienQuery()
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    Thang = filter.Thang
                }));
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPost]
        //[Authorize(Roles = Role.USER)]
        //public async Task<IActionResult> Create(CreatePhuCapParameter filter)
        //{
        //    ClaimsPrincipal currentUser = this.User;
        //    var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

        //    var resp = _accountService.GetLoginUser(currentEmail);

        //    if (resp.Result.Succeeded)
        //    {
        //        return Ok(await Mediator.Send(new CreatePhuCapCommand
        //        {
        //            NhanVienId = (Guid)resp.Result.Data.NhanVienId,
        //            LoaiPhuCapId = filter.LoaiPhuCapId,
        //            ThoiGianBatDau = filter.ThoiGianBatDau,
        //            ThoiGianKetThuc = filter.ThoiGianKetThuc,
        //            MoTa = filter.MoTa
        //        }));
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost("CreatePhuCapCountDay")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> CreatePhuCapCountDay(CreatePhuCapsCountDayParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync((Guid)resp.Result.Data.NhanVienId);
                return Ok(await Mediator.Send(new CreatePhuCapsCountDayCommand
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    NguoiXetDuyetCap1Id = nhanvien.XetDuyetCap1,
                    NguoiXetDuyetCap2Id = nhanvien.XetDuyetCap2,
                    LoaiPhuCapId = filter.LoaiPhuCapId,
                    MoTa = filter.MoTa,
                    ThoiGianBatDau = filter.ThoiGianBatDau,
                    ThoiGianKetThuc = filter.ThoiGianKetThuc
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("XetDuyetPhuCap")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post_XetDuyetPhuCap(XetDuyetPhuCapsParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetPhuCapsCommand
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    PhanLoai = filter.PhanLoai,
                    TrangThai = filter.TrangThai,
                    DanhSachXetDuyet = filter.DanhSachXetDuyet
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("XetDuyetPhuCapC1C2")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> XetDuyetPhuCapC1C2(XetDuyetPhuCapsC1C2Parameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetPhuCapsC1C2Command
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    TrangThai = filter.TrangThai,
                    DanhSachXetDuyet = filter.DanhSachXetDuyet
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("HrXetDuyetPhuCap")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post_HrXetDuyetPhuCap(HrXetDuyetPhuCapsParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new HrXetDuyetPhuCapsCommand
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    HR_TrangThai = filter.HR_TrangThai,
                    DanhSachXetDuyet = filter.DanhSachXetDuyet
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePhuCapsParameter filter)
        {
            return Ok(await Mediator.Send(new UpdatePhuCapCommand
            {
                Id = id,
                LoaiPhuCapId = filter.LoaiPhuCapId,
                ThoiGianBatDau = filter.ThoiGianBatDau,
                ThoiGianKetThuc = filter.ThoiGianKetThuc,
                MoTa = filter.MoTa
            }));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePhuCapByGuidCommand { Id = id }));
        }
    }
}
