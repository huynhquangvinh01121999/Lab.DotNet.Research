using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace API.Customer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly string[] customers = new[]
        {
            "Emily", "James", "Sophia", "William", "Olivia", "Benjamin", "Mia", "Ethan", "Ava", "Alexander", "Isabella", "Michael", "Charlotte", "Daniel", "Amelia", "Matthew", "Harper", "Andrew", "Abigail", "Christopher", "Ella", "David", "Grace", "Sarah", "Luke", "Elizabeth", "Joseph", "Sofia", "Samuel", "Victoria"
        };

        [HttpGet]
        public IActionResult Get()
        {
            // sleep 5s để test cache
            Thread.Sleep(5000);
            return Ok(customers);
        }
    }
}
