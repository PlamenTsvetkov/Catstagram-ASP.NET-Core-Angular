namespace Catstagram.Server.Features.Cats
{
    using Microsoft.AspNetCore.Mvc;

    using Catstagram.Server.Infrastructure.Extensions;
    using Catstagram.Server.Features.Cats.Models;
    using Microsoft.AspNetCore.Authorization;
    using System.Reflection.Metadata.Ecma335;

    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.catService = catService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CatListingResponseModel>> Mine()
        {
            var userId = this.User.GetId();

            return await this.catService.ByUser(userId);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();

            var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }
    }
}
