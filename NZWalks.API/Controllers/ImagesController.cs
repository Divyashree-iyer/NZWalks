using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Business.Interfaces;
using NZWalks.Domain.DTO;
using NZWalks.Domain.Entities;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }
        /// <summary>
        /// upload a file
        /// </summary>
        /// <param name="uploadRequestDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO uploadRequestDTO)
        {
            ValidateFileUpload(uploadRequestDTO);

            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = uploadRequestDTO.File,
                    FileDescription = uploadRequestDTO.FileDescription,
                    FileExtention = Path.GetExtension(uploadRequestDTO.File.FileName),
                    FileSizeInBytes = uploadRequestDTO.File.Length,
                    FileName = uploadRequestDTO.FileName,
                };
                await _imageService.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// validate if image to be uploaded meets certain criteria
        /// </summary>
        /// <param name="uploadRequestDTO"></param>
        private void ValidateFileUpload(ImageUploadRequestDTO uploadRequestDTO)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtentions.Contains(Path.GetExtension(uploadRequestDTO.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported File Extention");
            }
            //if length more than 10 mega bytes
            if(uploadRequestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File Size exceeds 10MB. Please Upload a smaller file.");
            }
        }
    }
}
