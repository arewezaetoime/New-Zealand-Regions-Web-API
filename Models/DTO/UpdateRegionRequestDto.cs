using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "The name has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "The name has to be maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The name has to be minimum of 3 characters")]
        [MaxLength(50, ErrorMessage = "The name has to be maximum of 50 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
