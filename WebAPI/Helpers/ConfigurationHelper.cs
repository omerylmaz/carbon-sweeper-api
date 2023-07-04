using Persistence;
namespace WebAPI.Helpers
{
    public static class ConfigurationHelper
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddPersistenceDI();
        }
    }
}
