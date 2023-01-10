namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Features.Cats.Models;

    public interface ICatService
    {
        public  Task<int> Create(string imageUrl, string description, string userId);

        public Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

        public Task<CatDetailsServiceModel> Details(int catId);
    }
}
