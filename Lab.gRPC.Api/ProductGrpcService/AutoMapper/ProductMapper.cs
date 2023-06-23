using AutoMapper;
using Product.Infracstructure.Entities;
using ProductGrpcService;

namespace Product.Infracstructure.AutoMapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Offer, OfferDetailViewModel>().ReverseMap();
            CreateMap<Offer, CreateDetailModel>().ReverseMap();
        }
    }
}
