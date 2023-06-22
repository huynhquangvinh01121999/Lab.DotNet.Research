using System;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.NhanViens.Commands.CreateNhanVien;
using EsuhaiHRM.Application.Features.NhanViens.Commands.DeleteNhanVienById;
using EsuhaiHRM.Application.Features.NhanViens.Commands.UpdateNhanVien;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetNhanVienById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EsuhaiHRM.WebApi.Models;
using EsuhaiHRM.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForExport;
using EsuhaiHRM.Application.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NhanVienController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> Get([FromQuery] GetAllNhanViensParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllNhanViensQuery() { PageSize = filter.PageSize
                                                                     , PageNumber = filter.PageNumber
                                                                     , SortParam = filter.SortParam
                                                                     , FilterParams = filter.FilterParams
                                                                     , SearchParam = filter.SearchParam}));
        }

        // GET: api/<controller>
        [HttpGet("GetListForPublic")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetListNvForPublic([FromQuery] GetAllNhanViensParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllNhanViensForPublicQuery() { PageSize = filter.PageSize
                                                                              , PageNumber = filter.PageNumber
                                                                              , SortParam = filter.SortParam
                                                                              , FilterParams = filter.FilterParams
                                                                              , SearchParam = filter.SearchParam }));
        }

        // GET: api/<controller>
        [HttpGet("GetListBirthDay")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetListBirthDay([FromQuery] RequestParameter filter, DateTime fromDate, DateTime toDate)
        {
            return Ok(await Mediator.Send(new GetAllNhanViensForHomeQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                FromDate = fromDate,
                ToDate = toDate,
                BirthDay = true
            }));
        }

        // GET: api/<controller>
        [HttpGet("GetListThuViec")]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> GetListThuViec([FromQuery] RequestParameter filter, DateTime fromDate, DateTime toDate)
        {
            return Ok(await Mediator.Send(new GetAllNhanViensForHomeQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                FromDate = fromDate,
                ToDate = toDate,
                BirthDay = false
            }));
        }


        // GET: api/<controller>
        [HttpGet("GetListForExport")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> GetListNvForExport([FromQuery] GetAllNhanViensParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllNhanViensForExportQuery(){ PageSize = filter.PageSize
                                                                             , PageNumber = filter.PageNumber
                                                                             , SortParam = filter.SortParam
                                                                             , FilterParams = filter.FilterParams
                                                                             , SearchParam = filter.SearchParam }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetNhanVienByIdQuery { Guid = id }));
        }

        // GET: api/<controller>
        [HttpGet("GetNhanVienForDropDown")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> GetNhanVienForDropDown([FromQuery] GetAllNhanViensForDropDownParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllNhanViensForDropDownQuery(){ PageSize = filter.PageSize
                                                                               , PageNumber = filter.PageNumber
                                                                               , PhongBanId = filter.PhongBanId
                                                                               }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Role.DBNS_ADD)]
        public async Task<IActionResult> Post(CreateNhanVienCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.DBNS_EDIT)]
        public async Task<IActionResult> Put(Guid id, UpdateNhanVienCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.DBNS_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteNhanVienByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        [HttpPut("DisableById/{id}")]
        [Authorize(Roles = Role.DBNS_EDIT)]
        public async Task<IActionResult> Disable(Guid id, DisableNhanVienByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdateAvatar")]
        [Authorize(Roles = Role.DBNS_EDIT)]
        public async Task<IActionResult> UpdateAvatar(Guid id, string fileName)
        {
            return Ok(await Mediator.Send(new UpdateNhanVienAvatarCommand { NhanVienId = id, Avatar = fileName }));
        }

        // PUT api/<controller>/5
        [HttpPut("DeleteAvatar")]
        [Authorize(Roles = Role.DBNS_EDIT)]
        public async Task<IActionResult> DeleteAvatar(Guid id, DeleteNhanVienAvatarCommand command)
        {
            if (id != command.NhanVienId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateAccount")]
        //[Authorize(Roles = Role.SUPERADMIN_ADMIN)]
        public async Task<ActionResult> CreateAccountForUser(CreateAccountForUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
