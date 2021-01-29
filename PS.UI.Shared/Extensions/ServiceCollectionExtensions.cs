using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.Shared.HttpClients;
using PS.UI.Shared.Secrets;
using PS.UI.Shared.ViewModels;
using System.IO;
using System.Reflection;

namespace PS.UI.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.production.json", optional: true);

#if DEBUG
            builder.AddUserSecrets(Assembly.GetExecutingAssembly());
#endif

            var configuration = builder.Build();

            services.AddHttpClients();
            services.AddViewModels();
            var syncfusionSecrets = configuration.GetSection(nameof(SyncfusionSecrets)).Get<SyncfusionSecrets>();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionSecrets.License);
            return services;
        }

        /// <summary>
        /// Configures all of the view models for dependency injection in this list
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<TestViewModel>();
            return services;
        }
    }
}