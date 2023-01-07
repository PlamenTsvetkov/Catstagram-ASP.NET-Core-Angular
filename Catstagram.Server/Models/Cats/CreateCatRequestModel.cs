namespace Catstagram.Server.Models.Cats
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Validation.Cat;

    public class CreateCatRequestModel
    {
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Desciption { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
