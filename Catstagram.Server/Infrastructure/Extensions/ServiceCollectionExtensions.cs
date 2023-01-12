namespace Catstagram.Server.Infrastructure.Extensions
{
    using Catstagram.Server.Data.Models;
    using Catstagram.Server.Data;
    using Microsoft.AspNetCore.Identity;
    using System.Reflection.Metadata.Ecma335;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Catstagram.Server.Features.Identity;
    using Catstagram.Server.Features.Cats;
    using Microsoft.OpenApi.Models;
    using Catstagram.Server.Infrastructure.Services;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                 {
                     options.Password.RequireDigit = false;
                     options.Password.RequireLowercase = false;
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequireUppercase = false;
                     options.Password.RequiredLength = 6;
                 })
                 .AddEntityFrameworkStores<CatstagramDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
                      this IServiceCollection services,
                      AppSettings appSettings)
        {
            
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           return  services.AddTransient<IIdentityService, IdentityService>()
                            .AddScoped<ICurrentUserService, CurrentUserService>()
                            .AddTransient<ICatService, CatService>();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services) 
            => services.AddSwaggerGen(c =>
                                     {
                                         c.SwaggerDoc(
                                             "v1",
                                             new OpenApiInfo 
                                             { 
                                                 Title = "My Catstagram API", 
                                                 Version = "v1" 
                                             }
                                             );
                                     });

        public static void  AddApiControllers(this IServiceCollection services)
        => services
            .AddControllers(option =>
                                     option
                                     .Filters.Add<ModelOrNotFoundActionFilter>());
    }
}
