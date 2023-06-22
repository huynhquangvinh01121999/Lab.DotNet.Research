using AutoMapper;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.CreateViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.DeleteViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.UpdateViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ViecBenNgoaiController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ViecBenNgoaiController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /*
         * Method: POST
         * Params: CreateViecBenNgoaiParameters
         * Description: API Dang ky Viec Ben Ngoai
         */
        [HttpPost]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Post(CreateViecBenNgoaiParameters filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var command = _mapper.Map<CreateViecBenNgoaiCommand>(filter);
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
         * Params: Guid, UpdateViecBenNgoaiParameters
         * Description: API Cap nhat don dang ky Viec Ben Ngoai
         */
        [HttpPut("{id}")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Put(Guid id, UpdateViecBenNgoaiParameters filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var command = _mapper.Map<UpdateViecBenNgoaiCommand>(filter);
                command.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                command.Id = id;
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
         * Description: API Xoa don Viec Ben Ngoai
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
                return Ok(await Mediator.Send(new DeleteViecBenNgoaiCommand
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
         * Description: API Get chi tiet Viec Ben Ngoai
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
                return Ok(await Mediator.Send(new GetDetailViecBenNgoaiQuery
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
         * Params: GetViecBenNgoaisNotHrViewParameter
         * Description: API Get danh sach don Việc bên ngoài view TPB
         */
        [HttpGet("GetAllNotViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetViecBenNgoaisNotHrViewParameter filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentEmail = currentUser.FindFirst(ClaimTypes.Email).Value;

            var resp = _accountService.GetLoginUser(currentEmail);

            if (resp.Result.Succeeded)
            {
                var query = _mapper.Map<GetViecBenNgoaisNotHrViewQuery>(filter);
                query.NhanVienId = (Guid)resp.Result.Data.NhanVienId;
                return Ok(await Mediator.Send(query));
            }
            else
            {
                return NotFound();
            }
        }

        /*
         * Method: GET
         * Params: GetViecBenNgoaiHrViewParameter
         * Description: API Get danh sach don Việc bên ngoài view HR
         */
        [HttpGet("GetAllViewHR")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get([FromQuery] GetViecBenNgoaiHrViewParameter filter)
        {
            var query = _mapper.Map<GetViecBenNgoaiHrViewQuery>(filter);
            return Ok(await Mediator.Send(query));
        }
    }
}
