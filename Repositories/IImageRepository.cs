using NZWaks.API.Models.Domain;

namespace NZWaks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
