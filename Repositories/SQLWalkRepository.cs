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
            return await dbContext.Walks.ToListAsync();
        }
    }
}
