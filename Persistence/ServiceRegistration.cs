

using Application.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repos;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceDI(this IServiceCollection services)
        {
            services.AddDbContext<CarbonSweeperDbContext>(o => o.UseNpgsql(Constants.ConnectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }));
            //services.AddScoped<Application.Repos.IWriteRepo, WriteRepo>();
            //services.AddScoped<Application.Repos.IReadRepo, ReadRepo>();


        }
    }
}
