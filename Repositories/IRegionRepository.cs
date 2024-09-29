using NZWaks.API.Models.Domain;

namespace NZWaks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
    }
}
