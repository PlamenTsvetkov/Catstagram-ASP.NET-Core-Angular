namespace Catstagram.Server.Features.Profiles.Models
{
    using Catstagram.Server.Data.Models;

    public class ProfileServiceModel
    {
        public string Name { get; set; }


        public string ProfilePhotoUrl { get; set; }


        public bool IsPrivate { get; set; }
    }
}
