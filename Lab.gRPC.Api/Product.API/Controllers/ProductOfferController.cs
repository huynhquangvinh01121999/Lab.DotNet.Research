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
    public class ProductOfferController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly ProductService.ProductServiceClient _client;
        private readonly IConfiguration _configuration;

        public ProductOfferController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel =
                GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:OfferServiceUrl"));
            _client = new ProductService.ProductServiceClient(_channel);
        }

        [HttpGet("getofferlist")]
        public async Task<Offers> GetOfferListAsync()
        {
            try
            {
                var response = await _client.GetOfferListAsync(new Empty { });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpGet("getofferbyid")]
        public async Task<OfferDetail> GetOfferByIdAsync(int Id)
        {
            try
            {
                var request = new GetOfferDetailRequest
                {
                    ProductId = Id
                };

                var response = await _client.GetOfferAsync(request);

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpPost("addoffer")]
        public async Task<OfferDetail> AddOfferAsync(Offer offer)
        {
            try
            {
                var offerDetail = new OfferDetail
                {
                    Id = offer.Id,
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                };

                var response = await _client.CreateOfferAsync(new CreateOfferDetailRequest()
                {
                    Offer = offerDetail
                });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpPut("updateoffer")]
        public async Task<OfferDetail> UpdateOfferAsync(Offer offer)
        {
            try
            {
                var offerDetail = new OfferDetail
                {
                    Id = offer.Id,
                    ProductName = offer.ProductName,
                    OfferDescription = offer.OfferDescription
                };

                var response = await _client.UpdateOfferAsync(new UpdateOfferDetailRequest()
                {
                    Offer = offerDetail
                });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpDelete("deleteoffer")]
        public async Task<DeleteOfferDetailResponse> DeleteOfferAsync(int Id)
        {
            try
            {
                var response = await _client.DeleteOfferAsync(new DeleteOfferDetailRequest()
                {
                    ProductId = Id
                });
                return response;
            }
            catch
            {

            }
            return null;
        }
    }
}
