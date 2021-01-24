using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared.Extensions;

namespace PS.UI.Blazor
{
    public static class Container
    {
        public static IServiceCollection Create(IServiceCollection serviceCollection)
        {
            ConfigureServices(serviceCollection);
            return serviceCollection;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedServices();
        }
    }
}