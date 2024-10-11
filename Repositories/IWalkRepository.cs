using NZWaks.API.Models.Domain;
using NZWaks.API.Models.DTO;

namespace NZWaks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walkDomainModel);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
