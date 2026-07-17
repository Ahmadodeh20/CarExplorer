using CarExplorer.Services.Interfaces;
using CarExplorer.Services.Service;

namespace CarExplorer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICarService, CarService>();
            return services;
        }
    }
}
