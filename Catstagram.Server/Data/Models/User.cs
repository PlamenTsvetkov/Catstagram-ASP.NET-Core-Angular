namespace Catstagram.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Cats = new HashSet<Cat>();
        }

        public IEnumerable<Cat> Cats { get; }
    }
}
