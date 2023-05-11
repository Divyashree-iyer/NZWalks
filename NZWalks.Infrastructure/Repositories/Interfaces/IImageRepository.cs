using NZWalks.Domain.Entities;

namespace NZWalks.Infrastructure.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
