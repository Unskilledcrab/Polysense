using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared;
using System;

namespace PS.UI.WPF
{
    public static class Container
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Create()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedServices();
            services.AddViews();
        }
    }
}