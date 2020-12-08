using Microsoft.Extensions.DependencyInjection;

namespace PS.Shared.HttpClients
{
    public static class DIConfigure
    {
        /// <summary>
        /// Configures all of the http clients for the API for dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<PoliticianClient>();
            services.AddHttpClient<ScraperTextClient>();
            return services;
        }
    }
}