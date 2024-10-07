using Microsoft.EntityFrameworkCore;
using NZWaks.API.Data;
using NZWaks.API.Models.Domain;

namespace NZWaks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbContext;

        public SQLWalkRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk = dbContext.Walks.FirstOrDefault(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalkDomainModel = dbContext.Walks.FirstOrDefault(x => x.Id == id);

            if (existingWalkDomainModel == null)
            {
                return null;
            }
            existingWalkDomainModel.Name = walk.Name;
            existingWalkDomainModel.Description = walk.Description;
            existingWalkDomainModel.LengthInKm = walk.LengthInKm;
            existingWalkDomainModel.WalkImageUrl = walk.WalkImageUrl;

            await dbContext.SaveChangesAsync();
            return existingWalkDomainModel;

        }
    }
}
