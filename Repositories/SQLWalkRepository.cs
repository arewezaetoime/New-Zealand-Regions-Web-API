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

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true)
        {
            var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // Apply filtering to all
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
                
            }

            return await walks.ToListAsync();

            // return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
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