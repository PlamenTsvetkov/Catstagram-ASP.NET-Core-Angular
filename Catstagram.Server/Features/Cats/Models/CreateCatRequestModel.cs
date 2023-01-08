﻿namespace Catstagram.Server.Features.Cats.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Validation.Cat;

    public class CreateCatRequestModel
    {
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}