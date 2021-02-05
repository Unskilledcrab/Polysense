using Microsoft.Extensions.DependencyInjection;
using PS.UI.WPF.ViewModels;
using PS.UI.WPF.Views;

namespace PS.UI.WPF
{
    public static class Configure
    {
        /// <summary>
        /// Configures all of the views for dependency injection. Make sure that views that depend
        /// on other views in their constructors are placed BEFORE those views in this list
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddSingleton<TestWindow>();
            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddScoped<TestViewModel>();
            return services;
        }
    }
}