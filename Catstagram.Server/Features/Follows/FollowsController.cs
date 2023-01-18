namespace Catstagram.Server.Features.Follows
{
    using Catstagram.Server.Features.Follows.Models;
    using Catstagram.Server.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;

    public class FollowsController : ApiController
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IFollowService followService;

        public FollowsController(
            ICurrentUserService currentUserService,
            IFollowService followService)
        {
            this.currentUserService = currentUserService;
            this.followService = followService;
        }

        [HttpPost]
        public async Task<ActionResult> Follow(FollowRequestModel model)
        {
            var result = await this.followService.Follow(
                model.UserId,
                this.currentUserService.GetId());

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
