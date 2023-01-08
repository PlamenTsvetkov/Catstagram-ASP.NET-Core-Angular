﻿namespace Catstagram.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Validation.Cat;

    public class Cat
    {

        public int Id { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}