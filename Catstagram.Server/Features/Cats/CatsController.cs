namespace Catstagram.Server.Features.Cats
{
    using Microsoft.AspNetCore.Mvc;

    using Catstagram.Server.Infrastructure.Extensions;
    using Catstagram.Server.Features.Cats.Models;
    using Microsoft.AspNetCore.Authorization;

    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.catService = catService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = User.GetId();

            var id = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), id);
        }
    }
}
