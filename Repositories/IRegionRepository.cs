using NZWaks.API.Models.Domain;
using NZWaks.API.Models.DTO;

namespace NZWaks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
    }
}
