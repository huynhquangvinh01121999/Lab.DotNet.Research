using EsuhaiHRM.Application.Features.LoaiPhuCaps.Queries.GetAllLoaiPhuCap;
using EsuhaiHRM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LoaiPhuCapController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Role.USER)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllLoaiPhuCapQuery()));
        }
    }
}
