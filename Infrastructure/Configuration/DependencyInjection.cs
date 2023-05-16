using Application.Interfaces;
using Infrastructure.Logging;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbName = configuration["CosmosDataBase"];
            var connectionString = configuration["CosmosConnectionString"];

            services.AddDbContext<ProductCatalogDbContext>(options =>
                options.UseCosmos(connectionString!, dbName!)
                );
            services.AddScoped<IProductCatalogDbContext>(provider => provider.GetRequiredService<ProductCatalogDbContext>());
            services.AddScoped(typeof(ILogger<>), typeof(NLogLogger<>));
            return services;
        }

        public static IWebHostBuilder AddInfrastructureHostConfiguration(this IWebHostBuilder host)
        {
            host.UseNLog();
            return host;
        }
    }
}
