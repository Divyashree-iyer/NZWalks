using Microsoft.EntityFrameworkCore;
using NZWalks.Domain.Entities;
using NZWalks.Infrastructure.Context;
using NZWalks.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Infrastructure.Repositories.Implementations
{
    public class SQLRegionRepository :IRegionRepository
    {
        private readonly NZWalksDbContext  _context;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.FindAsync(id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }
        public async Task<Region?> UpdateRegionAsync(Guid id, Region updateRegion)
        {
            var existingRegion = await _context.Regions.FindAsync(id);

            if(existingRegion== null)
            {
                return null;
            }

            existingRegion.Code = updateRegion.Code;
            existingRegion.Name = updateRegion.Name;
            existingRegion.RegionImageUrl = updateRegion.RegionImageUrl;
            await _context.SaveChangesAsync();

            return updateRegion;
        }
        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }
    }
}
