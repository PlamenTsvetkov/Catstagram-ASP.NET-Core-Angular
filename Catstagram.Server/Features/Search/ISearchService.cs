namespace Catstagram.Server.Features.Search
{
    using Catstagram.Server.Features.Search.Models;

    public interface ISearchService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query);
    }
}
