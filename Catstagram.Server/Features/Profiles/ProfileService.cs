namespace Catstagram.Server.Features.Profiles
{
    using Catstagram.Server.Data;
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Features.Profiles.Models;
    using Catstagram.Server.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using static Catstagram.Server.Data.Validation;
    using User = Data.Models.User;



    public class ProfileService : IProfileService
    {
        private readonly CatstagramDbContext data;

        public ProfileService(CatstagramDbContext data)
        {
            this.data = data;
        }
        public async Task<ProfileServiceModel> ByUser(string userId)
        => await this.data
            .Users
            .Where(u => u.Id == userId)
            .Select(u => new ProfileServiceModel
            {
                Name = u.Profile.Name,
                ProfilePhotoUrl = u.Profile.ProfilePhotoUrl,
                WebSite = u.Profile.WebSite,
                Biography = u.Profile.Biography,
                Gender = u.Profile.Gender.ToString(),
                IsPrivate = u.Profile.IsPrivate,
            })
            .FirstOrDefaultAsync();

        public async Task<Result> Update(
            string userId, 
            string email, 
            string userName, 
            string name, 
            string profilePhotoUrl, 
            string webSite, 
            string biography, 
            Gender gender, 
            bool isPrivate)
        {
            var user = await this.data.Users.FindAsync(userId);

            if (user == null)
            {
                return "User does not exist.";
            }

            var resultEmail = await this.ChangeEmail(user, userId, email);

            if (resultEmail.Failure)
            {
                return resultEmail;
            }
            

           var resiltUserName= await this.ChangeUserName(user, userId, userName);

            if (resiltUserName.Failure)
            {
                return resiltUserName;
            }

            this.ChangeProfile(
                user, name, 
                profilePhotoUrl, 
                webSite, 
                biography, 
                gender, 
                isPrivate);
           
            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Result> ChangeEmail(User user,string userId, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExists = await this
                    .data
                    .Users
                    .AnyAsync(u => u.Id != userId &&
                              u.Email == email);

                if (emailExists)
                {
                    return "The provided e-mail is already taken.";
                }

                user.Email = email;
            }
            return true;
        }

        private async Task<Result> ChangeUserName(User user, string userId, string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName) && user.UserName != userName)
            {
                var userNameExists = await this
                    .data
                    .Users
                    .AnyAsync(u => u.Id != userId &&
                              u.UserName == userName);

                if (userNameExists)
                {
                    return "The provided username is already taken.";
                }

                user.UserName = userName;
            }
            return true;
        }

        private void ChangeProfile(
            User user,
            string name,
            string profilePhotoUrl,
            string webSite,
            string biography,
            Gender gender,
            bool isPrivate)
        {
            if (user.Profile.Name != name)
            {
                user.Profile.Name = name;
            }

            if (user.Profile.ProfilePhotoUrl != profilePhotoUrl)
            {
                user.Profile.ProfilePhotoUrl = profilePhotoUrl;
            }

            if (user.Profile.WebSite != webSite)
            {
                user.Profile.WebSite = webSite;
            }

            if (user.Profile.Biography != biography)
            {
                user.Profile.Biography = biography;
            }

            if (user.Profile.Gender != gender)
            {
                user.Profile.Gender = gender;
            }

            if (user.Profile.IsPrivate != isPrivate)
            {
                user.Profile.IsPrivate = isPrivate;
            }
        }
    }
}
