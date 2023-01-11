namespace Catstagram.Server.Features.Cats
{
    using Microsoft.AspNetCore.Mvc;

    using Catstagram.Server.Infrastructure.Extensions;
    using Catstagram.Server.Features.Cats.Models;
    using Microsoft.AspNetCore.Authorization;


    using static Infrastructure.WebConstants;
    [Authorize]
    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.catService = catService;
        }


        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.User.GetId();

            return await this.catService.ByUser(userId);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int catId)
        => await this.catService.Details(catId);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();

            var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCatRequestModel model)
        {
            var userId = this.User.GetId();

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
        public async Task<ActionResult> Delete(int catId)
        {
            var userId = this.User.GetId();

            var deleted = await this.catService.Delete(catId, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();

        }

    }
}
