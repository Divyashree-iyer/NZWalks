﻿using NZWalks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Business.Interfaces
{
    public interface IWalkService
    {
        Task<List<Walk>> GetAllAsync(string? filterOn , string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize );
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateWalkAsync(Walk Walk);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk updateWalk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
