using Microsoft.Extensions.DependencyInjection;
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
        public static void Views(IServiceCollection services)
        {
            services.AddSingleton<TestWindow>();
        }
    }
}