namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Features.Cats.Models;

    public interface ICatService
    {
        Task<int> Create(string imageUrl, string description, string userId);

       Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

       Task<CatDetailsServiceModel> Details(int catId);

       Task<bool> Update(int id, string description, string userId);

       Task<bool> Delete(int catId,string userId);
    }
}
