using Microsoft.EntityFrameworkCore;
using NZWaks.API.Data;
using NZWaks.API.Models.Domain;

namespace NZWaks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dBContext;

        public SQLRegionRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }
    }
}
