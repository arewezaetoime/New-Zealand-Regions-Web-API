using AutoMapper;
using NZWaks.API.Models.Domain;
using NZWaks.API.Models.Dto;


namespace NZWaks.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
