using AutoMapper;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.CreateNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.DeleteNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.UpdateNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepC1C2;
using EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepHr;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetDetailNghiPhep;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView;
using EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NghiPhepController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public NghiPhepController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /*
         * Method: POST
         * Params: CreateNghiPhepParameters
         * Description: API Dang ky don Nghi Phep
         */
        [HttpPost]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(CreateNghiPhepParameters filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var command = _mapper.Map<CreateNghiPhepCommand>(filter);
                command.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                return Ok(await Mediator.Send(command));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: PUT
         * Params: Guid, UpdateNghiPhepParameters
         * Description: API Cap nhat don Nghi Phep
         */
        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(Guid id, UpdateNghiPhepParameters filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var command = _mapper.Map<UpdateNghiPhepCommand>(filter);
                command.Id = id;
                command.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                return Ok(await Mediator.Send(command));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: DELETE
         * Params: id
         * Description: API Xoa don Nghi Phep
         */
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Delete(Guid id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new DeleteNghiPhepCommand
                {
                    Id = id,
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId
                }));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: GET
         * Params: id
         * Description: API Get chi tiet don Nghi Phep
         */
        [HttpGet("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get(Guid id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new GetDetailNghiPhepQuery
                {
                    Id = id,
                    NhanVienId = (Guid)resp.Result.Data.NhanVienId
                }));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: GET
         * Params: GetNghiPhepsHrViewParameter
         * Description: API Get danh sach don nghi phep view HR
         */
        [HttpGet("GetAllViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetNghiPhepsHrViewParameter filter)
        {
            var query = _mapper.Map<GetNghiPhepsHrViewQuery>(filter);
            return Ok(await Mediator.Send(query));
        }

        /*
         * Method: GET
         * Params: GetNghiPhepsNotHrViewParameter
         * Description: API Get danh sach don nghi phep view TPB
         */
        [HttpGet("GetAllNotViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetNghiPhepsNotHrViewParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var query = _mapper.Map<GetNghiPhepsNotHrViewQuery>(filter);
                query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                return Ok(await Mediator.Send(query));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: POST
         * Params: XetDuyetNghiPhepC1C2Parameter
         * Description: API Xét duyệt đơn nghỉ phép cấp TPB
         */
        [HttpPost("XetDuyetNghiPhepC1C2")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(XetDuyetNghiPhepC1C2Parameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetNghiPhepC1C2Command
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

        /*
         * Method: POST
         * Params: XetDuyetNghiPhepHrParameter
         * Description: API Xét duyệt đơn nghỉ phép cấp HR
         */
        [HttpPost("XetDuyetNghiPhepHr")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(XetDuyetNghiPhepHrParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                return Ok(await Mediator.Send(new XetDuyetNghiPhepHrCommand
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
    }
}
