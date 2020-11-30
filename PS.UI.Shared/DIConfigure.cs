using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared.ViewModels;

namespace PS.UI.Shared
{
    public static class DIConfigure
    {
        public static void Shared(IServiceCollection services)
        {
            PS.Shared.DIConfigure.Clients(services);
            ViewModels(services);
        }

        /// <summary>
        /// Configures all of the view models for dependency injection. Make sure that viewmodels
        /// that depend on other viewmodels in their constructors are placed BEFORE those viewmodels
        /// in this list
        /// </summary>
        /// <param name="services"></param>
        public static void ViewModels(IServiceCollection services)
        {
            services.AddTransient<TestViewModel>();
        }
    }
}