using BasicPayment.Common;
using BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayOrderController : ControllerBase
    {
        [HttpGet("GetToken")]
        public IActionResult Get()
        {
            return Ok(new { jwt = BaoKimApi.JWT });
        }

        [HttpGet]
        public RedirectResult Redirect(OrderSuccessResponse response)
        {
            return RedirectPermanent("https://www.google.com");
        }
    }
}
