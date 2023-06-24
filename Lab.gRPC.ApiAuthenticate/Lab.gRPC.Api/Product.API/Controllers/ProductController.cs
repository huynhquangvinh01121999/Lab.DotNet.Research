using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Product.API.Models;
using System.Threading.Tasks;
using ProductGrpcService;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Grpc.Core;

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
            //var header = new Metadata();
            //header.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ2aW5oaHEiLCJqdGkiOiI0ZWQxZDYyOC1jMDU0LTQ5NmMtOWZlOS04ZjA1ZjAzNTA5NTMiLCJlbWFpbCI6InZpbmhocUBlc3VoYWkuY29tIiwidWlkIjoiNEE2MkE4MjItMDY2Ny00RTY4LTk5NkEtNTAxMjA5MzI5ODMyIiwiaXAiOiIxOTIuMTY4LjI1LjI0Iiwicm9sZXMiOlsiTmd1b25VbmdWaWVuX0xBIiwiTmd1b25VbmdWaWVuX0JWIiwiTmd1b25VbmdWaWVuX1MiLCJOZ3VvblVuZ1ZpZW5fVEciLCJOZ3VvblVuZ1ZpZW5fQkQiLCJVc2VyIiwiTmd1b25VbmdWaWVuX1F1YW5MeU5ndW9uIiwiVElURF9Vc2VycyIsIk5ndW9uVW5nVmllbl9IQU4iXSwiZXhwIjoxNjg3NTg2NDc3LCJpc3MiOiJDb3JlSWRlbnRpdHkiLCJhdWQiOiJDb3JlSWRlbnRpdHlVc2VyIn0.tXxpxvxfOfYgo0SH5IfsXTDac3s8Le_Jqtj0uPeAZjw");
            var credentials = CallCredentials.FromInterceptor((c, m) =>
            {
                m.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ2aW5oaHEiLCJqdGkiOiI0ZWQxZDYyOC1jMDU0LTQ5NmMtOWZlOS04ZjA1ZjAzNTA5NTMiLCJlbWFpbCI6InZpbmhocUBlc3VoYWkuY29tIiwidWlkIjoiNEE2MkE4MjItMDY2Ny00RTY4LTk5NkEtNTAxMjA5MzI5ODMyIiwiaXAiOiIxOTIuMTY4LjI1LjI0Iiwicm9sZXMiOlsiTmd1b25VbmdWaWVuX0xBIiwiTmd1b25VbmdWaWVuX0JWIiwiTmd1b25VbmdWaWVuX1MiLCJOZ3VvblVuZ1ZpZW5fVEciLCJOZ3VvblVuZ1ZpZW5fQkQiLCJVc2VyIiwiTmd1b25VbmdWaWVuX1F1YW5MeU5ndW9uIiwiVElURF9Vc2VycyIsIk5ndW9uVW5nVmllbl9IQU4iXSwiZXhwIjoxNjg3NTg2NDc3LCJpc3MiOiJDb3JlSWRlbnRpdHkiLCJhdWQiOiJDb3JlSWRlbnRpdHlVc2VyIn0.tXxpxvxfOfYgo0SH5IfsXTDac3s8Le_Jqtj0uPeAZjw");
                return Task.CompletedTask;
            });
            _channel =
                GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:OfferServiceUrl"),
                                        new GrpcChannelOptions
                                        {
                                            Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
                                            //Credentials = ChannelCredentials.Insecure
                                        });
            _client = new ProductService.ProductServiceClient(_channel);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
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
