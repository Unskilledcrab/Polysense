using Microsoft.Extensions.DependencyInjection;
using PS.Shared.HttpClients;
using PS.UI.Shared.ViewModels;

namespace PS.UI.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddHttpClients();
            services.AddViewModels();
            return services;
        }

        /// <summary>
        /// Configures all of the view models for dependency injection in this list
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<TestViewModel>();
            services.AddTransient<CategorizeScrapedDataViewModel>();
            return services;
        }
    }
}