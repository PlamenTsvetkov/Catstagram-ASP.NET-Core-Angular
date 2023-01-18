namespace Catstagram.Server.Features.Follows
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using Catstagram.Server.Data;
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Infrastructure.Services;

    public class FollowService : IFollowService
    {
        private readonly CatstagramDbContext data;

        public FollowService(CatstagramDbContext data)
        {
            this.data = data;
        }


        public async Task<Result> Follow(string userId, string followerId)
        {
            var userAlreadyFollowed = await this.data
                .Follows
                .AnyAsync(f => f.UserId == userId && f.FollowerId == followerId);

            if (userAlreadyFollowed)
            {
                return "This user already followed";
            }


            var publicProfile = await this.data
                 .Users
                 .Where(u => u.Id == userId)
                 .Select(p => !p.Profile.IsPrivate)
                 .FirstOrDefaultAsync();

            this.data.Follows.Add(new Follow
            {
                UserId = userId,
                FollowerId = followerId,
                IsApproved = publicProfile
            });

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
