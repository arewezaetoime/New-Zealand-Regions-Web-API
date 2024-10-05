using NZWaks.API.Models.Domain;

namespace NZWaks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
    }
}
