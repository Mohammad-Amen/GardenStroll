using GardenStroll.Data;
using GardenStroll.Interfaces;
using GardenStroll.Services;
using Microsoft.EntityFrameworkCore;

namespace GardenStroll.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnections")); });
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
