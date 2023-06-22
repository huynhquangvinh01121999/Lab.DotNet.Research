using AutoMapper;
using Product.Infracstructure.Entities;
using ProductGrpcService;

namespace Product.Infracstructure.AutoMapper
{
    public class OfferMapper : Profile
    {
        public OfferMapper()
        {
            CreateMap<Offer, OfferDetail>().ReverseMap();
        }
    }
}
