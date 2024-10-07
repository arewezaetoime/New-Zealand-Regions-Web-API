using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWaks.API.Models.Domain;
using NZWaks.API.Models.DTO;
using NZWaks.API.Repositories;

namespace NZWaks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //Create Walk method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkDto createWalkDto)
        {
            // Map the DTO to a model instance
            Walk walkModel = mapper.Map<Walk>(createWalkDto);
            await walkRepository.CreateWalkAsync(walkModel);

            // Map the created walk model to a DTO for returning back to the client
            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walksDomainModels = await walkRepository.GetAllWalksAsync();

            if (walksDomainModels == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModels));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
            await walkRepository.UpdateWalkAsync(id, walkDomainModel);
            
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}