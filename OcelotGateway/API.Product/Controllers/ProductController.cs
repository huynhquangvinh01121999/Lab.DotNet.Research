using Microsoft.AspNetCore.Mvc;

namespace API.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] products = new[]
        {
            "Smartphone", "Laptop", "Tablet", "Smart TV", "Smart Watch", "Gaming Console", "Headphones", "Bluetooth Speaker", "Camera", "Drone", "Fitness Tracker", "E-Reader", "Portable Charger", "Virtual Assistant", "Wireless Earbuds", "Monitor", "Printer", "External Hard Drive", "Router", "Smart Home Hub"
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products);
        }
    }
}
