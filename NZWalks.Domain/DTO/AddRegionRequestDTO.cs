﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.Domain.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage="Code must have 3 characters!")]
        [MaxLength(3, ErrorMessage = "Code must have 3 characters only!")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name can't exceed 100 characters!")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
