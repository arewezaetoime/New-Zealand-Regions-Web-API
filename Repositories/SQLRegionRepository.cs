using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
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


        async Task<Region> IRegionRepository.CreateRegionAsync(Region addRegionModel)
        {
            await dBContext.Regions.AddAsync(addRegionModel);
            await dBContext.SaveChangesAsync();
            return addRegionModel;
        }


        async Task<Region> IRegionRepository.DeleteRegionAsync(Guid id)
        {
            var existingDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDomainModel == null)
            {
                return null;
            }

            dBContext.Regions.Remove(existingDomainModel);
            await dBContext.SaveChangesAsync();

            return existingDomainModel;
        }

        async Task<Region?> IRegionRepository.GetByIdAsync(Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<Region> IRegionRepository.UpdateRegionAsync(Guid id, Region region)
        {
            var existingDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDomainModel == null)
            {
                return null;
            }

            existingDomainModel.Name = region.Name;
            existingDomainModel.Code = region.Code;
            existingDomainModel.RegionImageUrl = region.RegionImageUrl;
            await dBContext.SaveChangesAsync();

            return existingDomainModel;
        }
    }
}
