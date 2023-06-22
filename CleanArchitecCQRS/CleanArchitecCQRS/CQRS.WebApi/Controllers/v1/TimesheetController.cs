using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EsuhaiHRM.Application.Features.Timesheets.Commands.CheckIn_Out;
using EsuhaiHRM.Application.Features.Timesheets.Commands.UpdateTimesheet;
using EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetC1C2;
using EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetHr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetAllTimesheets;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanC1C2;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanHr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2C1C2;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3C1C2;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TimesheetController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TimesheetController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetTimesheetByIdQuery { Id = id }));
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetAllTimesheetsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllTimesheetsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET: api/<controller>
        [HttpGet("GetByNhanVien")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTimesheetsByNhanVienParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;


            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetTimesheetsByNhanVienQuery() { NhanVienId = (Guid)resp.Result.Data.NhanVienId, Thang = filter.Thang, Nam = filter.Nam }));
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/<controller>
        //[HttpGet("GetByNhanVienHrView")]
        //[Authorize(Roles = Role.USER_HR)]
        //public async Task<IActionResult> GetByNhanVienHrView([FromQuery] GetTimesheetsByNhanVienParameter filter)
        //{
        //    return Ok(await Mediator.Send(new GetTimesheetsByNhanVienQuery() { NhanVienId = filter.NhanVienId, Thang = filter.Thang, Nam = filter.Nam }));
        //}

        [HttpGet("GetAllViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTimesheetsHrViewParameter filter)
        {
            var query = _mapper.Map<GetTimesheetsHrViewQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetAllNotViewHr")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTimesheetsNotHrViewParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var query = _mapper.Map<GetTimesheetsNotHrViewQuery>(filter);
                query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                return Ok(await Mediator.Send(query));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTimesheetPhongBan")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetTimesheetsPhongBanParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                if (currentUser.IsInRole(Role.USER_HR))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanHrQuery>(filter);
                    return Ok(await Mediator.Send(query));
                }

                if (currentUser.IsInRole(Role.User_C1C2))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanC1C2Query>(filter);
                    query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                    return Ok(await Mediator.Send(query));
                }

                return Unauthorized();
            }
            else
            {
                return NotFound();
            }
        }



        [HttpGet("GetTimesheetPhongBanV2")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetTimesheetsPhongBanV2([FromQuery] GetTimesheetsPhongBanParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                if (currentUser.IsInRole(Role.USER_HR))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanV2HrQuery>(filter);
                    return Ok(await Mediator.Send(query));
                }

                if (currentUser.IsInRole(Role.User_C1C2))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanV2C1C2Query>(filter);
                    query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                    return Ok(await Mediator.Send(query));
                }

                return Unauthorized();
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * @Description Api Get danh sach timesheet cua nv theo phong ban ver 3
         * @Method GET
         * @Return list paging
         * @Params GetTimesheetsPhongBanParameter
         */
        [HttpGet("GetTimesheetPhongBanV3")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetTimesheetsPhongBanV3([FromQuery] GetTimesheetsPhongBanParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                if (currentUser.IsInRole(Role.USER_HR))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanV3HrQuery>(filter);
                    return Ok(await Mediator.Send(query));
                }

                if (currentUser.IsInRole(Role.User_C1C2))
                {
                    var query = _mapper.Map<GetTimesheetsPhongBanV3C1C2Query>(filter);
                    query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                    return Ok(await Mediator.Send(query));
                }

                return Forbid();
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * @Description API chấm công giờ vào cho nhân viên
         * @Method Post
         */
        [HttpPost("CheckIn")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> CheckIn()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);
            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new CheckInCommand { NhanVienId = (Guid)resp.Result.Data.NhanVienId }));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * @Description API chấm công giờ ra cho nhân viên
         * @Method Post
         */
        [HttpPost("CheckOut")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> CheckOut()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);
            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new CheckOutCommand { NhanVienId = (Guid)resp.Result.Data.NhanVienId }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("XetDuyetTimesheetHr")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(XetDuyetTimesheetHrParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetTimesheetHrCommand
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

        [HttpPost("XetDuyetTimesheetC1C2")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(XetDuyetTimesheetC1C2Parameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetTimesheetC1C2Command
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

        [HttpPut("DieuChinh/{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(Guid id, DieuChinhTimesheetCommand command)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                if((Guid)resp.Result.Data.NhanVienId == command.NhanVienId && id==command.Id)
                {
                    return Ok(await Mediator.Send(command));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("HuyDieuChinh/{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(Guid id, HuyDieuChinhTimesheetCommand command)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                if ((Guid)resp.Result.Data.NhanVienId == command.NhanVienId && id == command.Id)
                {
                    return Ok(await Mediator.Send(command));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        // GET api/<controller>/5
        [HttpGet("DieuChinhDetail/{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetDieuChinhDetail(Guid id)
        {
            return Ok(await Mediator.Send(new GetDieuChinhTsDetailQuery { Id = id }));
        }
    }
}
