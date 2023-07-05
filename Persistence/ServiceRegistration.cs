

using Application.Repos;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repos;
using Persistence.Services;

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
            services.AddScoped<IWriteRepo<User>, WriteRepo<User>>();
            services.AddScoped<IReadRepo<User>, ReadRepo<User>>();
            services.AddScoped<IWriteRepo<GeneralConsumption>, WriteRepo<GeneralConsumption>>();
            services.AddScoped<IReadRepo<GeneralConsumption>, ReadRepo<GeneralConsumption>>();
            services.AddScoped<IWriteRepo<Transport>, WriteRepo<Transport>>();
            services.AddScoped<IReadRepo<Transport>, ReadRepo<Transport>>();
            services.AddScoped<IWriteRepo<House>, WriteRepo<House>>();
            services.AddScoped<IReadRepo<House>, ReadRepo<House>>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICalculationService, CalculationService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
