using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans;
using EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs;
using EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetHuyenXaById;
using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = Role.USER)]
    public class GlobalController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet("GetListDropDownPhongBan")]        
        public async Task<IActionResult> Get([FromQuery] GetAllPhongBansListDropDownParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllPhongBansListDropDownQuery(){
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber
            }));
        }

        //Get huyen theo tinh
        [HttpGet("GetHuyenByTinhId")]
        public async Task<IActionResult> GetHuyenByTinhId([FromQuery] GetAllTinhThanhsParameter filter, int tinhId)
        {
            return Ok(await Mediator.Send(new GetAllHuyensByTinhIdQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                TinhId = tinhId
            }));
        }

        //Get xa theo huyen
        [HttpGet("GetXaByHuyenId")]
        public async Task<IActionResult> GetXaByHuyenId([FromQuery] GetAllTinhThanhsParameter filter, int huyenId)
        {
            return Ok(await Mediator.Send(new GetAllXasByHuyenIdQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                HuyenId = huyenId
            }));
        }



    }
}
