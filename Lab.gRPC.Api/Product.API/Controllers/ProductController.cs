using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Product.API.Models;
using System.Threading.Tasks;
using ProductGrpcService;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly ProductService.ProductServiceClient _client;
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel =
                GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:OfferServiceUrl"));
            _client = new ProductService.ProductServiceClient(_channel);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _client.GetListAsync(new Empty { });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetByIdRequest
            {
                ProductId = id
            };

            var response = await _client.GetByIdAsync(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Offer offer)
        {
            var response = await _client.CreateAsync(new CreateRequest()
            {
                Offer = new CreateDetailModel
                {
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                }
            });

            return Created("", response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Offer offer)
        {
            var response = await _client.UpdateAsync(new UpdateRequest()
            {
                Offer = new OfferDetailViewModel
                {
                    Id = id,
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                }
            });

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync(new DeleteRequest()
            {
                ProductId = id
            });
            return Ok(response);
        }
    }
}
