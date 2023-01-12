namespace Catstagram.Server.Features.Profiles
{
    using Catstagram.Server.Features.Profiles.Models;

    public interface IProfileService
    {
        Task<ProfileServiceModel> ByUser(string userId);
    }
}
