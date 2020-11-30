using Microsoft.Extensions.DependencyInjection;
using PS.Shared.HttpClients;

namespace PS.Shared
{
    public static class DIConfigure
    {
        /// <summary>
        /// Configures all of the http clients for the API for dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static void Clients(IServiceCollection services)
        {
            services.AddHttpClient<PoliticianClient>();
        }
    }
}