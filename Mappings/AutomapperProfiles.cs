using AutoMapper;
using NZWaks.API.Models.Domain;
using NZWaks.API.Models.Dto;
using NZWaks.API.Models.DTO;


namespace NZWaks.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
