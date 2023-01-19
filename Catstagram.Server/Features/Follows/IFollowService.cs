namespace Catstagram.Server.Features.Follows
{
    using Catstagram.Server.Infrastructure.Services;

    public interface IFollowService
    {
        Task<Result> Follow(string userId, string followerId);

        Task<bool> IsFollower(string userId, string followerId);
    }
}
