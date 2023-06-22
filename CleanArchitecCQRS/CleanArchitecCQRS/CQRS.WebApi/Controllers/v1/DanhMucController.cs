using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.DanhMucs.Queries.GetAllDanhMucs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DanhMucController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllDanhMucsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllDanhMucsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

    }
}
