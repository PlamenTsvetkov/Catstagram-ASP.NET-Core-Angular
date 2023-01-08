namespace Catstagram.Server.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("DefaultConnection");
            
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
             IConfiguration configuration)
        {
            var applicatopmSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicatopmSettingsConfiguration);
            return applicatopmSettingsConfiguration.Get<AppSettings>();
             
        }
    }
}
