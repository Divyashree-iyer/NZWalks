using Microsoft.EntityFrameworkCore;
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
    public class WalkService : IWalkService
    {
        private IWalkRepository _walkRepository;
        public WalkService(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;   
        }
        public async Task<List<Walk>> GetAllAsync()
        {
            return await _walkRepository.GetAllAsync();
        }
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _walkRepository.GetByIdAsync(id);
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            return await _walkRepository.CreateWalkAsync(walk); 
        }
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk updateWalk)
        {
            return await _walkRepository.UpdateWalkAsync(id, updateWalk);
        }
        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            return await _walkRepository.DeleteWalkAsync(id);
        }
    }
}
