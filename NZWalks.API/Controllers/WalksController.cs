using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.Business.Implementations;
using NZWalks.Business.Interfaces;
using NZWalks.Domain.DTO;
using NZWalks.Domain.Entities;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private IWalkService _walkService;
        private IMapper _mapper;
        public WalksController(IWalkService walkService, IMapper mapper)
        {
            _walkService = walkService;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await _walkService.GetAllAsync();
            var walksDto = _mapper.Map<List<WalkDTO>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await _walkService.GetByIdAsync(id);
            if (walk == null)
                return NotFound();

            var walkDto = _mapper.Map<WalkDTO>(walk);
            return Ok(walkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDTO addWalkRequestDTO) 
        {
      
            //Map AddWalkRequestDTO to Walk Domain Model
            var walk = _mapper.Map<Walk>(addWalkRequestDTO);
            await _walkService.CreateWalkAsync(walk);
            var walkDto = _mapper.Map<WalkDTO>(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walkDto);
      
        }
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updatedWalkDto)
        {
            var walkDomainModel = _mapper.Map<Walk>(updatedWalkDto);
            walkDomainModel = await _walkService.UpdateWalkAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkService.DeleteWalkAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
