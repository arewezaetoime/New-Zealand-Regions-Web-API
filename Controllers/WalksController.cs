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
        private readonly SQLWalkRepository walkRepository;

        public WalksController(IMapper mapper, SQLWalkRepository walkRepository)
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
    }
}
