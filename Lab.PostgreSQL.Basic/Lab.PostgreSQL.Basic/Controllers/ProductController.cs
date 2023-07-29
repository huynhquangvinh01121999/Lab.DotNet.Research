using Lab.PostgreSQL.Basic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab.PostgreSQL.Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositoryAsync _productRepositoryAsync;

        public ProductController(IProductRepositoryAsync productRepositoryAsync)
        {
            _productRepositoryAsync = productRepositoryAsync;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productRepositoryAsync.GetProducts());
        }
    }
}
