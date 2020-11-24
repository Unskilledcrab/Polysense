using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared;
using System;
using Xamarin.Forms;

namespace PS.UI.Xamarin
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static Shell Start()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            return ServiceProvider.GetService<AppShell>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            DIConfigure.Shared(services);
            Configure.Views(services);
            services.AddTransient<AppShell>();
        }
    }
}