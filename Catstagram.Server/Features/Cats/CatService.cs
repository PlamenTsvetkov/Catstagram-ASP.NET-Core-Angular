namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Data;
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Features.Cats.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CatService : ICatService
    {
        private readonly CatstagramDbContext data;

        public CatService(CatstagramDbContext data)
        {
            this.data = data;
        }

        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                ImageUrl = imageUrl,
                Description = description,
                UserId = userId,
            };

            data.Cats.Add(cat);

            await data.SaveChangesAsync();

            return cat.Id;
        }

        public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId)
       => await this.data.Cats
            .Where(c => c.UserId == userId)
            .Select(c => new CatListingServiceModel
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
            })
            .ToListAsync();

        public async Task<CatDetailsServiceModel> Details(int catId)
        => await this.data
            .Cats
            .Where(c => c.Id == catId)
            .Select(c => new CatDetailsServiceModel
            {
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                UserId = c.UserId,
                UserUserName = c.User.UserName
            })
            .FirstOrDefaultAsync();

        public async Task<bool> Update(int catId, string description, string userId)
        {
            var cat = await this.ByIdAndByUserId(catId, userId);

            if (cat==null)
            {
                return false;
            }

            cat.Description = description;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int catId, string userId)
        {
            var cat = await this.ByIdAndByUserId(catId, userId);

            if (cat == null)
            {
                return false;
            }

            this.data.Cats.Remove(cat);

            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Cat> ByIdAndByUserId(int catId, string userId)
        => await this.data
               .Cats
               .Where(c => c.Id == catId && c.UserId == userId)
               .FirstOrDefaultAsync();
    }
}
