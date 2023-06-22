using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.DashBoards.Queries.GetAllDashBoardInfos;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DashBoardController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllDashBoardInfosQuery() { }));
        }

        [HttpGet("MonthList")]
        [Authorize(Roles = Role.HRM_VIEW)]
        public async Task<IActionResult> GetListNhanVienByMonth(int? year)
        {
            return Ok(await Mediator.Send(new GetListNhanVienByMonthQuery() { Year = year }));
        }
    }
}
