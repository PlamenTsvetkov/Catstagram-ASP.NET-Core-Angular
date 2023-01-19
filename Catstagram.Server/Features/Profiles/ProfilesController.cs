namespace Catstagram.Server.Features.Profiles
{
    using Catstagram.Server.Features.Follows;
    using Catstagram.Server.Features.Profiles.Models;
    using Catstagram.Server.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebConstants;

    public class ProfilesController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly IFollowService followService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(
            IProfileService profileService,
            IFollowService followService,
            ICurrentUserService currentUserService)
        {
            this.profileService = profileService;
            this.followService = followService;
            this.currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<ActionResult<ProfileServiceModel>> Mine()
        => await this.profileService.ByUser(this.currentUserService.GetId(), allInformation:true);

        [HttpGet]
        [Route(Id)]
        public async Task<ProfileServiceModel> Details(string id)
        {
            var includeAllInformation = await this.followService.IsFollower(id, currentUserService.GetId());

            if (!includeAllInformation)
            {
                includeAllInformation = !await this.profileService.isPublic(id);
            }
            return await this.profileService.ByUser(id, includeAllInformation);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.profileService.Update
                (
                userId,
                model.Email,
                model.UserName,
                model.Name,
                model.ProfilePhotoUrl,
                model.WebSite,
                model.Biography,
                model.Gender,
                model.IsPrivate);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
