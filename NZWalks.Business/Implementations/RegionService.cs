using NZWalks.Business.Interfaces;
using NZWalks.Domain.Entities;
using NZWalks.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Business.Implementations
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository regionRepository;
        
        public RegionService(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        public Task<Region> CreateRegionAsync(Region region)
        {
            return this.regionRepository.CreateRegionAsync(region);
        }

        public Task<Region?> DeleteRegionAsync(Guid id)
        {
            return this.regionRepository.DeleteRegionAsync(id);
        }

        public Task<List<Region>> GetAllAsync()
        {
            return this.regionRepository.GetAllAsync();
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            return this.regionRepository.GetByIdAsync(id);
        }

        public Task<Region?> UpdateRegionAsync(Guid id, Region updateRegion)
        {
            return this.regionRepository.UpdateRegionAsync(id, updateRegion);
        }
    }
}
