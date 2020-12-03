using Microsoft.Extensions.DependencyInjection;
using PS.UI.Xamarin.Views;

namespace PS.UI.Xamarin
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
            services.AddTransient<TestView>();
            return services;
        }
    }
}