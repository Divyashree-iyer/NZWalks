using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Domain.DTO
{
    public class AddRegionRequestDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
