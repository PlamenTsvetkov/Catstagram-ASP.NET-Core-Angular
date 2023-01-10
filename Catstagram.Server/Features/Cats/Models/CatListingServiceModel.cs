namespace Catstagram.Server.Features.Cats.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CatListingServiceModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
