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

        private readonly ICurrentUserService currentUserService;

        public CatsController(
            ICatService catService, 
            ICurrentUserService currentUser)
        {
            this.catService = catService;
            this.currentUserService = currentUser;
        }


        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.currentUserService.GetId();

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
            var userId = this.currentUserService.GetId();

            var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }


        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Update(int id, UpdateCatRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.catService.Update
                (
                id,
                model.Description,
                userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();

        }


        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.catService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

    }
}
