using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Commands.JobTongHopDuLieu;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuById;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuNhanVien;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetAllTongHopDuLieus;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TongHopDuLieuController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TongHopDuLieuController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTongHopDuLieuByIdQuery { Id = id }));
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetAllTongHopDuLieusParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllTongHopDuLieusQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET: api/<controller>
        [HttpGet("GetByNhanVien")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTongHopDuLieusByNhanVienParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetTongHopDuLieusByNhanVienQuery() { NhanVienId = (Guid)resp.Result.Data.NhanVienId, Thang = filter.Thang, Nam = filter.Nam }));
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/<controller>
        [HttpGet("GetByNhanVienHrView")]
        [Authorize(Roles = Role.USER_HR)]
        public async Task<IActionResult> GetByNhanVienHrView([FromQuery] GetTongHopDuLieusByNhanVienParameter filter)
        {
            return Ok(await Mediator.Send(new GetTongHopDuLieusByNhanVienQuery() { NhanVienId = filter.NhanVienId, Thang = filter.Thang, Nam = filter.Nam }));
        }

        // GET: api/<controller>
        [HttpGet("GetTongHopDuLieuNhanVien")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetTongHopDuLieuNhanVien([FromQuery] GetTongHopDuLieuNhanVienParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetTongHopDuLieuNhanVienQuery() { NhanVienId = (Guid)resp.Result.Data.NhanVienId, ThoiGian = filter.ThoiGian}));
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/<controller>
        [HttpGet("QuanLyNgayCong")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> QuanLyNgayCong([FromQuery] QuanLyNgayCongParameter filter)
        {
            var query = _mapper.Map<QuanLyNgayCongQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        // GET: api/<controller>
        [HttpPost("RunJob")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post([FromQuery] JobTongHopDuLieuParameter filter)
        {
            return Ok(await Mediator.Send(new JobTongHopDuLieuQuery { Thang = filter.Thang, Nam = filter.Nam}));
        }

        // GET: api/<controller>
        [HttpGet("GetTongHopNghi")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get(int thang, int nam)
        {
            return Ok(await Mediator.Send(new GetTongHopNghiQuery { Thang = thang, Nam = nam}));
        }

        // GET: api/<controller>
        [HttpGet("ExportTongHopNgayCong")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTongHopNgayCongQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
