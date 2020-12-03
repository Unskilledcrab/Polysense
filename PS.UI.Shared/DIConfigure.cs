using Microsoft.Extensions.DependencyInjection;
using PS.Shared;
using PS.UI.Shared.ViewModels;

namespace PS.UI.Shared
{
    public static class DIConfigure
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddHttpClients();
            services.AddViewModels();
        }

        /// <summary>
        /// Configures all of the view models for dependency injection in this list
        /// </summary>
        /// <param name="services"></param>
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<TestViewModel>();
        }
    }
}