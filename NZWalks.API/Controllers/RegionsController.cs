using Microsoft.AspNetCore.Mvc;
using NZWalks.Domain.DTO;
using NZWalks.Domain.Entities;
using NZWalks.Infrastructure.Context;
using System.Linq;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext _context;
        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            _context= nZWalksDbContext;
        }

        //Get All Regions
        //Route - https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = _context.Regions.ToList();
            var regionsDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDTO()
                { 
                    Id= region.Id,
                    Name = region.Name,
                    Code= region.Code,
                    RegionImageUrl= region.RegionImageUrl,
                });
            }
            return Ok(regionsDto);
        }

        //Get Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _context.Regions.Find(id);
            //var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
                return NotFound();

            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        //Post to create new Region
        // Post - https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };
            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();

            var regionDto = new RegionDTO()
            { 
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById),new { id = regionDomainModel.Id}, regionDto);
        }

        //Update Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updatedRegionDto)
        {
            var regionDomainModel = _context.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updatedRegionDto.Code;
            regionDomainModel.Name = updatedRegionDto.Name;
            regionDomainModel.RegionImageUrl = updatedRegionDto.RegionImageUrl;

            _context.SaveChanges();

            var regionDto = new RegionDTO()
            { 
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //Delete Region by ID
        //Route - https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = _context.Regions.Find(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(regionDomainModel);
            _context.SaveChanges();

            return Ok();
        }
    }

}
