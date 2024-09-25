using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWaks.API.Data;
using NZWaks.API.Models.Domain;
using NZWaks.API.Models.Dto;
using NZWaks.API.Models.DTO;

namespace NZWaks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public RegionsController(NZWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regionDomainModels = dbContext.Regions.ToList();

            //Map domain models to DTOs
            var regionsDtos = new List<RegionDto>();
            foreach (var region in regionDomainModels)
            {
                regionsDtos.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            // Return the DTO not the model
            return Ok(regionsDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            //alternative way to get the regionDomainModel
            // var region2 = dbContext.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map the domain models to DTOs
            var regionDtoById = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            // Return the DTO back to the client, but not the model itself 
            return Ok(regionDtoById);
        }

        // POST request - creating a new Region record and adding it to the database
        //https://localhost:portnumber/api/regions
        [HttpPost, ActionName("dai region, smeshko")]
        public IActionResult CreateNewRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Mapping the DTO to a model instance
            var addRegionModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(addRegionModel);
            dbContext.SaveChanges();

            // Map the added region model to a DTO for returning back to the client

            var returnDto = new RegionDto()
            {
                Id = addRegionModel.Id,
                Code = addRegionModel.Code,
                Name = addRegionModel.Name,
                RegionImageUrl = addRegionModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = returnDto.Id }, returnDto);
        }
    }
}

// This is a basic implementation of a GET endpoint for retrieving all regionDomainModels from the database.   