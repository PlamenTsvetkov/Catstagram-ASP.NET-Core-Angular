﻿namespace Catstagram.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Validation.Cat;

    public class Cat
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Desciption { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

    }
}
