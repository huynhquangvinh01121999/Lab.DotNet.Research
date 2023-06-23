using AutoMapper;
using Grpc.Core;
using Product.Infracstructure.Entities;
using Product.Infracstructure.IRepositories;
using System.Threading.Tasks;

namespace ProductGrpcService.Services
{
    public class ProductOfferService : ProductService.ProductServiceBase
    {
        private readonly IProductRepository _prductOfferService;
        private readonly IMapper _mapper;

        public ProductOfferService(IProductRepository prductOfferService, IMapper mapper)
        {
            _prductOfferService = prductOfferService;
            _mapper = mapper;
        }

        public async override Task<Offers> GetList(Empty request, ServerCallContext context)
        {
            var offersData = await _prductOfferService.GetListAsync();

            Offers response = new Offers();
            foreach (Offer offer in offersData)
            {
                response.Items.Add(_mapper.Map<OfferDetailViewModel>(offer));
            }

            return response;
        }

        public async override Task<OfferDetailViewModel> GetById(GetByIdRequest request, ServerCallContext context)
        {
            var offer = await _prductOfferService.GetByIdAsync(request.ProductId);
            var offerDetail = _mapper.Map<OfferDetailViewModel>(offer);
            return offerDetail;
        }

        public async override Task<OfferDetailViewModel> Create(CreateRequest request, ServerCallContext context)
        {
            var offer = _mapper.Map<Offer>(request.Offer);

            await _prductOfferService.AddAsync(offer);

            var offerDetail = _mapper.Map<OfferDetailViewModel>(offer);
            return offerDetail;
        }

        public async override Task<OfferDetailViewModel> Update(UpdateRequest request, ServerCallContext context)
        {
            var offer = _mapper.Map<Offer>(request.Offer);

            await _prductOfferService.UpdateAsync(offer);

            var offerDetail = _mapper.Map<OfferDetailViewModel>(offer);
            return offerDetail;
        }

        public async override Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            var isDeleted = await _prductOfferService.DeleteAsync(request.ProductId);
            var response = new DeleteResponse
            {
                IsDelete = isDeleted
            };

            return response;
        }
    }
}
