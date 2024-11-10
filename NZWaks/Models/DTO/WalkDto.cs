using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Navigation properties
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
