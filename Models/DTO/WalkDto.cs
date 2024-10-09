﻿using NZWaks.API.Models.Domain;
using NZWaks.API.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace NZWaks.API.Models.DTO
{
    public class WalkDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
        [MaxLength(100, ErrorMessage = "Max lenght should be 100 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
        [MaxLength(255, ErrorMessage = "Max length should be 255 characters")]
        public string Description { get; set; }
        [Required]
        
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Navigation properties
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
