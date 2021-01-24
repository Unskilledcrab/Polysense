using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared.Extensions;
using System;

namespace PS.UI.Xamarin
{
    public static class IoC
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static T Get<T>() => ServiceProvider.GetRequiredService<T>();

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
            services.AddTransient<AppShell>();
        }
    }
}