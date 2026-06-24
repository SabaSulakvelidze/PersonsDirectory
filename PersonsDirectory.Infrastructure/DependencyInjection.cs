using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Infrastructure.Persistence;
using PersonsDirectory.Infrastructure.Storage;

namespace PersonsDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config,
            string contentRootPath)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IFileStorage>(_ => new FileStorage(contentRootPath));

            return services;
        }
    }
}
