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
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;
        public SQLWalkRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<List<Walk>> GetAllAsync() 
        {
            return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync<Walk>();
        }
        public async Task<Walk?> GetByIdAsync(Guid id) 
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=> x.Id == id);
        }
        public async Task<Walk> CreateWalkAsync(Walk walk) 
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk updateWalk) 
        {
            var existingWalk = await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = updateWalk.Name;
            existingWalk.Description = updateWalk.Description;
            existingWalk.LengthInKm = updateWalk.LengthInKm;
            existingWalk.WalkImageUrl = updateWalk.WalkImageUrl;
            existingWalk.DifficultyId = updateWalk.DifficultyId;
            existingWalk.RegionId = updateWalk.RegionId;
            existingWalk.Difficulty = await _context.Difficulties.FindAsync(updateWalk.DifficultyId);
            existingWalk.Region = await _context.Regions.FindAsync(updateWalk.RegionId);
            await _context.SaveChangesAsync();
            return existingWalk;
    }
        public async Task<Walk?> DeleteWalkAsync(Guid id) 
        {
            var existingWalk = await _context.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            _context.Walks.Remove(existingWalk);
            await _context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
