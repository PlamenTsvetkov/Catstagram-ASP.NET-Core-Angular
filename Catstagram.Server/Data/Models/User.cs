namespace Catstagram.Server.Data.Models
{
    using Catstagram.Server.Data.Models.Base;
    using Microsoft.AspNetCore.Identity;
    using System.Diagnostics.CodeAnalysis;

   

    public class User : IdentityUser, IEntity
    {
        public User()
        {
            this.Cats = new HashSet<Cat>();
        }

        public Profile Profile { get; set; }


        public DateTime CreatedOn { get; set; }

        [AllowNull]
        public string CreatedBy { get; set; }


        public DateTime? ModifiedOn { get; set; }

        [AllowNull]
        public string ModifiedBy { get; set; }


        public IEnumerable<Cat> Cats { get; }
    }
}
