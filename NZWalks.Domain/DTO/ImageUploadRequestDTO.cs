using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Domain.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
    }
}
