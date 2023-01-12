namespace Catstagram.Server.Features.Profiles
{
    using Catstagram.Server.Data;
    using Catstagram.Server.Features.Profiles.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private readonly CatstagramDbContext data;

        public ProfileService(CatstagramDbContext data)
        {
            this.data = data;
        }
        public async Task<ProfileServiceModel> ByUser(string userId)
        => await this.data
            .Users
            .Where(u => u.Id == userId)
            .Select(u => new ProfileServiceModel
            {
                Name = u.Profile.Name,
                ProfilePhotoUrl = u.Profile.ProfilePhotoUrl,
                WebSite = u.Profile.WebSite,
                Biography = u.Profile.Biography,
                Gender = u.Profile.Gender.ToString(),
                IsPrivate = u.Profile.IsPrivate,
            })
            .FirstOrDefaultAsync();
    }
}
