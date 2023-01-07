namespace Catstagram.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Catstagram.Server.Infrastructure.Extensions;
    using Catstagram.Server.Models.Cats;
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Data;

    public class CatsController : ApiController
    {
        private readonly CatstagramDbContext data;

        public CatsController(CatstagramDbContext data)
        {
            this.data = data;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {
            var userId = this.User.GetId();

            var cat = new Cat
            {
                Desciption = model.Description,
                ImageUrl = model.ImageUrl,
                UserId = userId,
            };

            this.data.Add(cat);

            await this.data.SaveChangesAsync();

            return Created(nameof(this.Create), cat.Id);
        }
    }
}
