using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared;
using PS.UI.Shared.Clients;
using PS.UI.Shared.ViewModels;
using PS.UI.WPF.Views;

namespace PS.UI.WPF
{
    public static class Configure
    {
        /// <summary>
        /// Configures all of the http clients for the API for dependency injection. Make sure that
        /// clients that depend on other clients in thier constructors are placed BEFORE those
        /// clients in this list
        /// </summary>
        /// <param name="services"></param>
        public static void Clients(IServiceCollection services)
        {
            services.AddSingleton<PolysenseClient>();
            services.AddSingleton<PoliticianClient>();
        }

        /// <summary>
        /// Configures all of the view models for dependency injection. Make sure that viewmodels
        /// that depend on other viewmodels in their constructors are placed BEFORE those viewmodels
        /// in this list
        /// </summary>
        /// <param name="services"></param>
        public static void ViewModels(IServiceCollection services)
        {
            services.AddSingleton<TestViewModel>();
        }

        /// <summary>
        /// Configures all of the views for dependency injection. Make sure that views that depend
        /// on other views in their constructors are placed BEFORE those views in this list
        /// </summary>
        /// <param name="services"></param>
        public static void Views(IServiceCollection services)
        {
            services.AddSingleton<TestWindow>();
        }
    }
}