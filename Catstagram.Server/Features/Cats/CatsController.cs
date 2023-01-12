namespace Catstagram.Server.Features.Cats
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Catstagram.Server.Features.Cats.Models;
    using Catstagram.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        private readonly ICurrentUserService currentUser;

        public CatsController(
            ICatService catService, 
            ICurrentUserService currentUser)
        {
            this.catService = catService;
            this.currentUser = currentUser;
        }


        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.currentUser.GetId();

            return await this.catService.ByUser(userId);
        }


        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
            => await this.catService.Details(id);


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }


        [HttpPut]
        public async Task<ActionResult> Update(UpdateCatRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var updated = await this.catService.Update
                (
                model.Id,
                model.Description,
                userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();

        }


        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUser.GetId();

            var deleted = await this.catService.Delete(id, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
