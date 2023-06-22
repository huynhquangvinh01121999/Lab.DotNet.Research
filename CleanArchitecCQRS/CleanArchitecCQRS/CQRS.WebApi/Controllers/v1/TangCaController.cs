using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EsuhaiHRM.Application.Features.TangCas.Commands.CreateTangCa;
using EsuhaiHRM.Application.Features.TangCas.Commands.DeleteTangCaByGuid;
using EsuhaiHRM.Application.Features.TangCas.Commands.HrXetDuyetTangCa;
using EsuhaiHRM.Application.Features.TangCas.Commands.UpdateTangCa;
using EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCa;
using EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCaC1C2;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCas;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TangCaController : BaseApiController
    {
        private readonly IAccountService _accountService; 
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        private readonly IMapper _mapper;

        public TangCaController(IAccountService accountService, INhanVienRepositoryAsync nhanVienRepositoryAsync, IMapper mapper)
        {
            _accountService = accountService;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
            _mapper = mapper;
        }

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //[Authorize(Roles = Role.USER)]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    return Ok(await Mediator.Send(new GetTangCaByIdQuery { Id = id }));
        //}

        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTangCasParameter filter)
        {
            return Ok(await Mediator.Send(new GetTangCasQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET: api/<controller>
        [HttpGet("GetByNhanVien")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTangCasByNhanVienParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetTangCasByNhanVienQuery() { NhanVienId = (Guid)resp.Result.Data.NhanVienId, NgayLamViec = filter.NgayLamViec}));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllNotViewHr")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTangCasNotHrViewParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetTangCasNotHrViewQuery()
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

        [HttpGet("GetAllViewHR")]
        //[Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTangCasHrViewParameter filter)
        {
            var query = _mapper.Map<GetTangCasHrViewQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(CreateTangCaParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync((Guid)resp.Result.Data.NhanVienId);
                
                return Ok(await Mediator.Send(new CreateTangCaCommand
                {
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId,
                    NguoiXetDuyetCap1Id = nhanvien.XetDuyetCap1,
                    NguoiXetDuyetCap2Id = nhanvien.XetDuyetCap2,
                    NgayTangCa = filter.NgayTangCa,
                    MoTa = filter.MoTa,
                    ThoiGianBatDau = filter.ThoiGianBatDau,
                    ThoiGianKetThuc = filter.ThoiGianKetThuc,
                    SoGioDangKy = filter.SoGioDangKy
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("XetDuyetTangCa")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post_XetDuyetTangCa(XetDuyetTangCaParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetTangCaCommand
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

        [HttpPost("HrXetDuyetTangCa")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post_HrXetDuyetTangCa(HrXetDuyetTangCaParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new HrXetDuyetTangCaCommand
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

        [HttpPost("XetDuyetTangCaC1C2")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> XetDuyetTangCaC1C2(XetDuyetTangCaC1C2Parameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetTangCaC1C2Command
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

        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTangCaParameter filter)
        {
            var command = _mapper.Map<UpdateTangCaCommand>(filter);
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteTangCaByGuidCommand { Id = id }));
        }
    }
}
