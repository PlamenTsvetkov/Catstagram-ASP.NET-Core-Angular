namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Data;
    using Catstagram.Server.Data.Models;
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

            data.Add(cat);

            await data.SaveChangesAsync();

            return cat.Id;
        }
    }
}
