using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            logger.LogInformation("GetAllRegions action method was invoked");

            var regionDomainModels = await regionRepository.GetAllRegionsAsync();

            //Map domain models to DTOs
            var regionsDtos = mapper.Map<List<RegionDto>>(regionDomainModels);
            // Return the DTO not the model
            return Ok(regionsDtos);
        }


        [HttpGet]
        //[Authorize(Roles = "Reader")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.GetByIdAsync(id);
            //alternative way to get the regionDomainModel
            // var region2 = dbContext.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map the domain models to DTOs
            var regionDtoById = mapper.Map<RegionDto>(regionDomainModel);
            // Return the DTO back to the client, but not the model itself 
            return Ok(regionDtoById);
        }


        // POST request - creating a new Region record and adding it to the database
        //https://localhost:portnumber/api/regions

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Mapping the DTO to a model instance
            var addRegionModel = mapper.Map<Region>(addRegionRequestDto);

            var repositoryResponseModel = await regionRepository.CreateRegionAsync(addRegionModel);

            // Map the added region model to a DTO for returning back to the client
            if (repositoryResponseModel == null)
            {
                return NotFound();
            }

            var returnDto = mapper.Map<RegionDto>(addRegionModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = returnDto.Id }, returnDto);
        }



        // PUT request - creating a new Region record and adding it to the database
        //https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //map the dto to a domain model and pass it to the repository 
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            //check if the model is null then proceed accordingly 
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //convert the model to dto and return 
            var returnRegionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(returnRegionDto);

        }

        // DELETE request - deleting a Region record from the database
        [HttpDelete]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegionAsync([FromRoute] Guid id)
        {

            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound(id);
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}