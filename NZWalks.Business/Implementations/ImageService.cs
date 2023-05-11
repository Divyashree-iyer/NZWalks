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
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        

        public async Task<Image> Upload(Image image)
        {
            return await _imageRepository.Upload(image);
        }
    }
}
