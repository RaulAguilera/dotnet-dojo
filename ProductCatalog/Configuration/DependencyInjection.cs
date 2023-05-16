using Application.Interfaces;
using Infrastructure.Persistence;

namespace API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
