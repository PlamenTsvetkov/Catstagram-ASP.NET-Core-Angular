namespace Catstagram.Server.Controllers
{
    using Catstagram.Server.Models.Cats;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel cat)
        {

        }
    }
}
