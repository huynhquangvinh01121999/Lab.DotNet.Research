using AutoMapper;
using EsuhaiSchedule.API.Parameters;
using EsuhaiSchedule.Application.DTOs;

namespace EsuhaiSchedule.API.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AddOrUpdateParameters, AddOrUpdateDtos>();
        }
    }
}
