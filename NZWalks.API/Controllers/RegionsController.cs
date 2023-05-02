using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.Business.Interfaces;
using NZWalks.Domain.DTO;
using NZWalks.Domain.Entities;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private IRegionService _regionService;
        private IMapper _mapper;
        public RegionsController(IRegionService regionService, IMapper mapper)
        {
            _regionService= regionService;
            _mapper = mapper;
        }

        //Get All Regions
        //Route - https://localhost:portnumber/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionService.GetAllAsync();
            var regionsDto = _mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDto);
        }

        //Get Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionService.GetByIdAsync(id);
            if (region == null)
                return NotFound();

            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        //Post to create new Region
        // Post - https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
    
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDTO);
            await _regionService.CreateRegionAsync(regionDomainModel);
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            
        }

        //Update Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updatedRegionDto)
        {

            var regionDomainModel = _mapper.Map<Region>(updatedRegionDto);
            regionDomainModel = await _regionService.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);

        }

        //Delete Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionService.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}
