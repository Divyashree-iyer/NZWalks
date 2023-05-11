
using NZWalks.Domain.Entities;

namespace NZWalks.Business.Interfaces
{
    public interface IImageService
    {
        Task<Image> Upload(Image image);
    }
}
