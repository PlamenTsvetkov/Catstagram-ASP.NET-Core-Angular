namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Features.Cats.Models;
    using Catstagram.Server.Infrastructure.Services;

    public interface ICatService
    {
        Task<int> Create(string imageUrl, string description, string userId);

       Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

       Task<CatDetailsServiceModel> Details(int catId);

       Task<Result> Update(int id, string description, string userId);

       Task<Result> Delete(int catId,string userId);
    }
}
